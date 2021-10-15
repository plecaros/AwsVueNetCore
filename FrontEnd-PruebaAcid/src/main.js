import Vue from 'vue'
import App from './App.vue'
import router from './router'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
// Import Bootstrap an BootstrapVue CSS files (order is important)
import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap-vue/dist/bootstrap-vue.css"
import VueAxios from 'vue-axios'
import Axios from 'axios'
import GoogleSignInButton from 'vue-google-signin-button-directive'
const gauthOption = {
  clientId: '224533072990-if4k4ton7e5l98qpa21vb2q1kc3jance.apps.googleusercontent.com'
}
Vue.use(gauthOption)
Vue.config.productionTip = false
Vue.use(BootstrapVue);
Vue.use(IconsPlugin);
Vue.use( VueAxios, Axios);

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

new Vue({
  GoogleSignInButton,
  router,
  render: h => h(App)
}).$mount('#app')
