<template>
    <div>
        <Header />
            <div class="container">
                

                <form action="" class="form-horizontal">

                    <div class="form-group left row">
                      <div class="col">
                            <label for="" class="control-label col-sm-3">Id</label>
                            <div class="col-sm-12">
                                <input type="text" class="form-control" name="id" id="id" v-model="form.id" placeholder="Id">
                            </div>
                        </div>
                        <div class="col">
                          <label for="" class="control-label col-sm-5">Nombre</label>
                          <div class="col-sm-12">
                              <input type="text" class="form-control" name="nombre" id="nombre" v-model="form.nombre" placeholder="Nombre">
                          </div>
                        </div> 
                    </div>
                    <div class="form-group left row">
                      <div class="col">
                            <label for="" class="control-label col-sm-6">Apellido Paterno</label>
                            <div class="col-sm-12">
                                <input type="text" class="form-control" name="aPaterno" id="aPaterno" v-model="form.aPaterno" placeholder="Apellido paterno">
                            </div>
                        </div>
                        <div class="col">
                          <label for="" class="control-label col-sm-6">Apellido Materno</label>
                          <div class="col-sm-12">
                              <input type="text" class="form-control" name="aMaterno" id="aMaterno" v-model="form.aMaterno" placeholder="Apellido materno">
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
                              <input type="text" class="form-control" name="correo" id="correo" v-model="form.correo" placeholder="Correo">
                          </div>
                        </div> 
                    </div>
                    <div class="form-group">
                      <button type="button" class="btn btn-primary" v-on:click="guardar()" >Guardar</button>
                      <button type="button" class="btn btn-dark margen" v-on:click="salir()"  >Salir</button>
                    </div> 
                </form>


            </div>
            <Footer/>
    </div>
</template>
<script>
import Header from '@/components/Header.vue'
import Footer from '@/components/Footer.vue'
import axios from 'axios';
export default {
    name:"Nuevo",
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
    components:{
        Header,
        Footer
    },
    methods:{
        guardar(){
            this.form.token = localStorage.getItem("token");
            axios.post("http://apiserv-dev.sa-east-1.elasticbeanstalk.com/api/Alumno/AgregaAlumno",this.form)
            .then(data =>{
                if(data.data[0].success==true){
                this.makeToast("Hecho",data.data[0].message,data.data[0].success);
                this.$router.push("/dashboard");
                }else{
                    this.makeToast("Error",data.data[0].message,data.data[0].success);
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
        }
        
    }
}
</script>
<style scoped>
.left{
    text-align:  left;
}
</style>