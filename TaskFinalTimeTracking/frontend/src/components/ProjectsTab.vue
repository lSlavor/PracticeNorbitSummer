<template>
  <div class="page">

    <div class="header">
      <div>
        <h1>📁 Проекты</h1>
        <p>Управление рабочими проектами</p>
      </div>
    </div>


    <div class="card">

      <table>

        <thead>
          <tr>
            <th>Название</th>
            <th>Код</th>
            <th>Статус</th>
            <th></th>
          </tr>
        </thead>


        <tbody>

          <tr v-for="p in projects" :key="p.id">

            <td>
              <strong>{{p.name}}</strong>
            </td>


            <td>
              <span class="code">
                {{p.code}}
              </span>
            </td>


            <td>
              <span 
                class="status"
                :class="p.isActive ? 'active':'inactive'"
              >
                {{p.isActive ? 'Активен':'Отключен'}}
              </span>
            </td>


            <td>
              <button 
                class="delete"
                @click="remove(p.id)"
              >
                🗑 Удалить
              </button>
            </td>


          </tr>

        </tbody>

      </table>

    </div>



    <div class="card form-card">

      <h2>Создать проект</h2>


      <form @submit.prevent="create">


        <div class="field">
          <label>Название</label>
          <input 
            v-model="form.name"
            placeholder="Например: CRM система"
            required
          >
        </div>


        <div class="field">
          <label>Код проекта</label>

          <input
            v-model="form.code"
            placeholder="CRM001"
            required
          >

        </div>


        <label class="checkbox">

          <input 
            type="checkbox"
            v-model="form.isActive"
          >

          Активный проект

        </label>



        <button class="create">
          + Добавить проект
        </button>


      </form>

    </div>



    <div 
      class="error"
      v-if="error"
    >
      {{error}}
    </div>


  </div>
</template>


<script setup>

import {ref,onMounted} from 'vue'
import api from '../services/api'


const emit = defineEmits(['changed'])


const projects = ref([])

const error = ref('')


const form = ref({
  name:'',
  code:'',
  isActive:true
})


async function load(){

 const res = await api.get('/Projects')

 projects.value=res.data

}



async function create(){

 try{

 await api.post('/Projects',form.value)


 form.value={
  name:'',
  code:'',
  isActive:true
 }


 await load()

 emit('changed')


 }

 catch(e){

 error.value='Не удалось создать проект'

 }


}



async function remove(id){

 await api.delete(`/Projects/${id}`)

 await load()

 emit('changed')

}



onMounted(load)

</script>



<style scoped>


.page{

background:#f6f8fc;

padding:25px;

min-height:100vh;

}



.header{

margin-bottom:25px;

}


.header h1{

font-size:28px;

margin:0;

}


.header p{

color:#718096;

}



.card{

background:white;

border-radius:16px;

padding:20px;

box-shadow:
0 8px 25px rgba(0,0,0,.06);

margin-bottom:25px;

}



table{

width:100%;

border-collapse:collapse;

}



th{

text-align:left;

font-size:13px;

color:#718096;

padding:14px;

border-bottom:1px solid #eee;

}



td{

padding:15px;

border-bottom:1px solid #f1f1f1;

}



tr:hover{

background:#fafafa;

}



.code{

background:#eef2ff;

color:#4f46e5;

padding:5px 10px;

border-radius:8px;

font-size:13px;

}



.status{

padding:6px 12px;

border-radius:20px;

font-size:13px;

font-weight:600;

}



.active{

background:#dcfce7;

color:#15803d;

}



.inactive{

background:#fee2e2;

color:#b91c1c;

}



.delete{

background:#fee2e2;

border:none;

color:#dc2626;

padding:8px 14px;

border-radius:10px;

cursor:pointer;

}



.delete:hover{

background:#fecaca;

}



.form-card h2{

margin-top:0;

}



form{

display:flex;

gap:20px;

align-items:end;

flex-wrap:wrap;

}



.field{

display:flex;

flex-direction:column;

gap:7px;

}



input{

padding:12px;

border:1px solid #ddd;

border-radius:10px;

font-size:14px;

}



input:focus{

outline:none;

border-color:#6366f1;

}



.checkbox{

display:flex;

gap:8px;

align-items:center;

}



.create{

background:#4f46e5;

color:white;

border:none;

padding:12px 22px;

border-radius:12px;

cursor:pointer;

font-weight:600;

}



.create:hover{

background:#4338ca;

}



.error{

background:#fee2e2;

color:#b91c1c;

padding:12px;

border-radius:12px;

}


</style>