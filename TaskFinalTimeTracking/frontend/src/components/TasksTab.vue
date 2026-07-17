<template>

<div class="page">


  <div class="header">

    <div>

      <h1>
        ✅ Задачи
      </h1>

      <p>
        Управление задачами внутри проектов
      </p>

    </div>

  </div>




  <!-- Таблица задач -->

  <div class="card">


    <table>


      <thead>

        <tr>

          <th>
            Название
          </th>

          <th>
            Проект
          </th>

          <th>
            Статус
          </th>

          <th></th>

        </tr>

      </thead>



      <tbody>


        <tr
          v-for="t in tasks"
          :key="t.id"
        >


          <td>

            <strong>
              {{t.name}}
            </strong>

          </td>



          <td>

            <span class="project">

              {{projectName(t.projectId)}}

            </span>


          </td>




          <td>


            <span
              class="status"
              :class="
              t.isActive
              ? 'active'
              :'inactive'
              "
            >

              {{t.isActive ? 'Активна':'Отключена'}}


            </span>


          </td>



          <td>


            <button
              class="delete"
              @click="remove(t.id)"
            >

              🗑 Удалить

            </button>


          </td>



        </tr>



      </tbody>


    </table>


  </div>






  <!-- Создание задачи -->


  <div class="card form-card">


    <h2>
      Создать задачу
    </h2>



    <form
      @submit.prevent="create"
    >


      <div class="field">


        <label>
          Название
        </label>


        <input
          v-model="form.name"
          placeholder="Например: Разработка API"
          required
        />


      </div>





      <div class="field">


        <label>
          Проект
        </label>


        <select
          v-model.number="form.projectId"
          required
        >

          <option
            disabled
            value=""
          >
            — выбрать проект —
          </option>


          <option
            v-for="p in projects"
            :key="p.id"
            :value="p.id"
          >

            {{p.name}}

          </option>


        </select>


      </div>





      <label class="checkbox">


        <input
          type="checkbox"
          v-model="form.isActive"
        />


        Активная задача


      </label>






      <button
        class="create"
      >

        + Добавить задачу


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


import {
  ref,
  onMounted
}
from 'vue'


import api from '../services/api'



const emit =
defineEmits(['changed'])




const tasks=ref([])

const projects=ref([])

const error=ref('')



const form=ref({

name:'',

projectId:'',

isActive:true

})





function projectName(id){


return projects.value
.find(
p=>p.id===id
)?.name ?? '—'


}






async function load(){


const [
tasksRes,
projectsRes

]=await Promise.all([


api.get('/Tasks'),


api.get('/Projects')


])



tasks.value=
tasksRes.data



projects.value=
projectsRes.data


}






async function create(){


error.value=''


try{


await api.post(
'/Tasks',
form.value
)




form.value={

name:'',

projectId:'',

isActive:true

}




await load()


emit('changed')


}

catch(e){


error.value =
'Не удалось создать задачу'


}


}






async function remove(id){


await api.delete(
`/Tasks/${id}`
)


await load()


emit('changed')


}





onMounted(load)



</script>







<style scoped>



.page{


padding:0;


}




.header{


margin-bottom:25px;


}



.header h1{


font-size:28px;


margin:0;


}



.header p{


color:#64748b;


}






.card{


background:white;


border-radius:20px;


padding:25px;


box-shadow:
0 10px 30px rgba(0,0,0,.06);


margin-bottom:25px;


}







table{


width:100%;


border-collapse:collapse;


}





th{


text-align:left;


font-size:13px;


color:#64748b;


padding:14px;


border-bottom:
1px solid #eee;


}





td{


padding:16px;


border-bottom:
1px solid #f1f1f1;


}





tr:hover{


background:#fafafa;


}





.project{


background:#ede9fe;


color:#7c3aed;


padding:6px 12px;


border-radius:10px;


font-size:13px;


font-weight:600;


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


color:#dc2626;


border:none;


padding:9px 14px;


border-radius:12px;


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





label{


font-size:14px;


color:#475569;


}





input,
select{


padding:12px;


border:1px solid #ddd;


border-radius:12px;


font-size:14px;


min-width:220px;


}





input:focus,
select:focus{


outline:none;


border-color:#6366f1;


}







.checkbox{


display:flex;


align-items:center;


gap:8px;


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