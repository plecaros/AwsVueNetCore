using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using ApiServ.Domain.Models;
using ApiServ.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiServ.Repositories
{
    public class AlumnoRepository : IAlumnoRepository
    {

        private readonly IAmazonDynamoDB _dynamoDbClient;
        private static readonly string tableName = "AlumnoTable";

        public AlumnoRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
        }

        public async Task AgregaAlumnosRepo(Alumno alumno)
        {
            try
            {

                var hora = DateTime.Now.ToString("HH:mm:ss");
                var fecha = DateTime.Now.ToString("dd-MM-yyyy");

                var fechaCompuesta = fecha + " " + hora;

                var request = new PutItemRequest
                {
                    TableName = tableName,
                    Item = new Dictionary<string, AttributeValue>()
                  {
                    {"Id", new AttributeValue {N = alumno.Id.ToString()}},
                    {"Nombre", new AttributeValue {S = alumno.Nombre}},
                    {"APaterno", new AttributeValue {S = alumno.APaterno}},
                    {"AMaterno", new AttributeValue {S = alumno.AMaterno}},
                    {"Direccion", new AttributeValue {S = alumno.Direccion}},
                    {"Correo", new AttributeValue {S = alumno.Correo}},
                    {"FechaCreacion", new AttributeValue {S = fechaCompuesta}}

                  }
                };

                var vari = await _dynamoDbClient.PutItemAsync(request);
            }
            catch (Exception exp) {

                throw new Exception(exp.ToString());
            }


        }

        public async Task<ItemsTableAlumnos> ObtenerAlumnosRepo(int? id)        
        {
            try
            {

                var queryRequest = RequestBuilder(id);

                var result = await ScanAsync(queryRequest);

                return new ItemsTableAlumnos
                {
                    Alumnos = result.Items.Select(Map).ToList()
                };
            
            }catch (Exception exp) {

                throw new Exception(exp.ToString());
            }
        }

        private Alumno Map(Dictionary<string, AttributeValue> result)
        {
            return new Alumno
            {
                Id = Convert.ToInt32(result["Id"].N),
                FechaCreacion = result["FechaCreacion"].S,
                Nombre = result["Nombre"].S,
                APaterno = result["APaterno"].S,
                AMaterno = result["AMaterno"].S,
                Direccion = result["Direccion"].S,
                Correo = result["Correo"].S

            };
        }

        private async Task<ScanResponse> ScanAsync(ScanRequest request)
        {
            var response = await _dynamoDbClient.ScanAsync(request);

            return response;
        }

        private ScanRequest RequestBuilder(int? id)
        {
            if (id.HasValue == false)
            {
                return new ScanRequest
                {
                    TableName = tableName
                };
            }

            return new ScanRequest
            {
                TableName = tableName,
                ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                    {
                        ":v_Id", new AttributeValue { N = id.ToString()}}

                },
                FilterExpression = "Id = :v_Id",
                ProjectionExpression = "Id, FechaCreacion, Nombre, APaterno, AMaterno, Direccion, Correo"
            };
        }



        public async Task<Alumno> ActualizaAlumnoRepo(Alumno alumno)
        {
            try {
                var response = await ObtenerAlumnosRepo(alumno.Id);

                var CurrentDireccion = response.Alumnos.Select(p => p.Direccion).FirstOrDefault();

                var CurrentCorreo = response.Alumnos.Select(p => p.Correo).FirstOrDefault();

                var fechaLlave = response.Alumnos.Select(p => p.FechaCreacion).FirstOrDefault();

                var request = RequestBuilderUpdate(alumno.Id, CurrentDireccion, alumno.Direccion, fechaLlave);

                var result = await UpdateItemAsync(request);

                return new Alumno
                {
                    Id = Convert.ToInt32(result.Attributes["Id"].N),
                    FechaCreacion = result.Attributes["FechaCreacion"].S,
                    Nombre = result.Attributes["Nombre"].S,
                    APaterno = result.Attributes["APaterno"].S,
                    AMaterno = result.Attributes["AMaterno"].S,
                    Direccion = result.Attributes["Direccion"].S,
                    Correo = result.Attributes["Correo"].S
                };

            }
            catch (Exception exp) {
                throw new Exception(exp.ToString());
            }
        }

        private UpdateItemRequest RequestBuilderUpdate(int Id, string CurrentDireccion, string ChangeDireccion, string fechaLlave)
        {


            var request = new UpdateItemRequest

            {
                Key = new Dictionary<string, AttributeValue>
                {
                    {
                        "Id", new AttributeValue
                        {
                            N = Id.ToString()
                        }
                    },
                    {
                        "FechaCreacion", new AttributeValue
                        {
                            S = fechaLlave
                        }
                    }
                },
                ExpressionAttributeNames = new Dictionary<string, string>
                {
                    {"#D", "Direccion"}

                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    {
                        ":newdireccion", new AttributeValue
                        {
                            S = ChangeDireccion
                        }
                    },
                    {
                        ":currdireccion", new AttributeValue
                        {
                            S = CurrentDireccion
                        }
                    }
                },

                UpdateExpression = "SET #D = :newdireccion",
                ConditionExpression = "#D = :currdireccion",

                TableName = tableName,
                ReturnValues = "ALL_NEW"
            };

            return request;
        }
        private async Task<UpdateItemResponse> UpdateItemAsync(UpdateItemRequest request)
        {
            var response = await _dynamoDbClient.UpdateItemAsync(request);

            return response;
        }


        /// <summary>
        /// Elimina fila.
        /// </summary>
        /// <param name="alumno"></param>
        /// <returns></returns>
        public async Task<bool> EliminaAlumnoRepo(Alumno alumno)
        {
            try
            {
                var response = await ObtenerAlumnosRepo(alumno.Id);

                var fechaLlave = response.Alumnos.Select(p => p.FechaCreacion).FirstOrDefault();


                Dictionary<string, AttributeValue> key = new Dictionary<string, AttributeValue>
{
                { "Id", new AttributeValue { N = alumno.Id.ToString()}},
                { "FechaCreacion", new AttributeValue { S = fechaLlave}}
            };

                // Create DeleteItem request
                DeleteItemRequest request = new DeleteItemRequest
                {
                    TableName = tableName,
                    Key = key
                };

                // Issue request
               var retorno = await _dynamoDbClient.DeleteItemAsync(request);

              return true;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.ToString());
            }
        }


    }
}
