<template>
        <div>
          <Header />
            <div class="container">
                <form action="" class="form-horizontal">
                    <div class="form-group left row">
                      <div class="col">
                            <label for="" class="control-label col-sm-3">Id</label>
                            <div class="col-sm-12">
                                <input type="text" class="form-control" disabled name="id" id="id" v-model="form.id" placeholder="Id">
                            </div>
                        </div>
                        <div class="col">
                          <label for="" class="control-label col-sm-5">Nombre</label>
                          <div class="col-sm-12">
                              <input type="text" class="form-control" disabled name="nombre" id="nombre" v-model="form.nombre" placeholder="Nombre">
                          </div>
                        </div> 
                    </div>
                    <div class="form-group left row">
                      <div class="col">
                            <label for="" class="control-label col-sm-6">Apellido Paterno</label>
                            <div class="col-sm-12">
                                <input type="text" class="form-control" disabled name="aPaterno" id="aPaterno" v-model="form.aPaterno" placeholder="Apellido paterno">
                            </div>
                        </div>
                        <div class="col">
                          <label for="" class="control-label col-sm-6">Apellido Materno</label>
                          <div class="col-sm-12">
                              <input type="text" class="form-control" disabled name="aMaterno" id="aMaterno" v-model="form.aMaterno" placeholder="Apellido materno">
                          </div>
                        </div> 
                    </div>
                    <div class="form-group left row">
                      <div class="col">
                            <label for="" class="control-label col-sm-6">Dirección</label>
                            <div class="col-sm-12">
                                <input type="text" class="form-control" name="direccion" id="direccion" v-model="form.direccion" placeholder="Dirección">
                            </div>
                        </div>
                        <div class="col">
                          <label for="" class="control-label col-sm-6">Correo</label>
                          <div class="col-sm-12">
                              <input type="text" class="form-control" disabled name="correo" id="correo" v-model="form.correo" placeholder="Correo">
                          </div>
                        </div> 
                    </div>


                    <div class="form-group">
                      <button type="button" class="btn btn-primary" v-on:click="editar()" >Editar</button>
                      <button type="button" class="btn btn-danger margen" v-on:click="eliminar()" >Eliminar</button>
                      <button type="button" class="btn btn-dark margen" v-on:click="salir()"  >Salir</button>
                    </div> 
                </form>
            </div>
          <!-- <Footer />   -->
        </div>
    
</template>
<script>
import Header from '@/components/Header.vue';
//import Footer from '@/components/Footer.vue';
import axios from 'axios';
export default {
  name:"Editar",
  components:{
    Header
    //Footer
  },
  data:function(){
    return {
        form:{
                "id" : "",
                "nombre": "", 
                "aPaterno" : "",
                "aMaterno":"",
                "direccion" :"",
                "correo" : "",
                "token" : "" 
        }
    }
  },
  methods:{
      editar(){
          axios.put("http://apiserv-dev.sa-east-1.elasticbeanstalk.com/api/Alumno/ActualizaAlumno?id=",this.form)
          .then( data =>{
          if(data.data[0].success==true)
            {
                this.makeToast("Hecho",data.data[0].message,data.data[0].success);
            }
            else{
                this.makeToast("Error",data.data[0].message);
            }
          }).catch( e =>{
                console.log(e);
                 this.makeToast("Error","Error al guardar","error");
            })
      },
      salir(){
        this.$router.push("/dashboard");
      },
      makeToast(titulo,texto,tipo) {
            this.toastCount++
            this.$bvToast.toast(texto, {
            title: titulo,
            variant: tipo,
            autoHideDelay: 5000,
            appendToast: true
            })
      },
      eliminar(){
        var enviar = {
            "id" : this.form.id,
            "token" : this.form.token
        };
        axios.delete("http://apiserv-dev.sa-east-1.elasticbeanstalk.com/api/Alumno/EliminaAlumno?id="+enviar.id)
        .then( datos => {
            console.log(datos);
           if(datos.data[0].success==true)
            {
                this.$router.push("/dashboard");
            }
            else{
                this.makeToast("Error",datos.data[0].message);
            }
        }).catch( e =>{
                console.log(e);
                 this.makeToast("Error","Error al guardar","error");
        })

      }
    
  },
  mounted:function(){
        this.form.id = this.$route.params.id;
        let direccion = "http://apiserv-dev.sa-east-1.elasticbeanstalk.com/api/Alumno/BuscaAlumno?id="+this.form.id;
        axios.get(direccion).then( data =>{
            if(data.data[0].success==true)
            {
                this.form.nombre = data.data[0].data[0].nombre;
                this.form.aPaterno = data.data[0].data[0].aPaterno;
                this.form.aMaterno = data.data[0].data[0].aMaterno;
                this.form.direccion = data.data[0].data[0].direccion;
                this.form.correo = data.data[0].data[0].correo;
                this.form.token = localStorage.getItem("token");
            }
            else{
                alert("Error: " + data.data[0].message);
            }
        }).catch(function (error){
            alert("Error de comunicacion con el servidor: " + error);
        })
    }
  }  
</script>
<style scoped>
 .left{
   text-align: left;
 };
 .margen{
   margin-left: 15px;
   margin-right: 15px;;
 }
 button.btn.btn-primary {
    margin-left: 20px;
}

button.btn.btn-danger.margen {
    margin-left: 20px;
    margin-right: 20px;
}
</style>