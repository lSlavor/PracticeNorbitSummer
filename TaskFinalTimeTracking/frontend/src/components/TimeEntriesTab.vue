<template>

<div class="page">


  <div class="header">

    <h1>
      🕒 Учет времени
    </h1>

    <p>
      Контроль рабочего времени сотрудников
    </p>

  </div>





  <!-- Фильтры -->

  <div class="card filters">


    <div class="field">


      <label>
        Период
      </label>


      <select
        v-model="filterMode"
        @change="load"
      >

        <option value="all">
          Всё время
        </option>

        <option value="day">
          День
        </option>

        <option value="month">
          Месяц
        </option>


      </select>


    </div>





    <div
      class="field"
      v-if="filterMode==='day'"
    >

      <label>
        Дата
      </label>


      <input
        type="date"
        v-model="filterDate"
        @change="load"
      />


    </div>





    <div
      class="field"
      v-if="filterMode==='month'"
    >

      <label>
        Месяц
      </label>


      <input
        type="month"
        v-model="filterMonth"
        @change="load"
      />


    </div>






    <div class="field">


      <label>
        Сотрудник
      </label>


      <input

        v-model="summaryEmployee"

        @change="loadSummary"

        placeholder="Введите ФИО"

      />


    </div>


  </div>








  <!-- Статус сотрудника -->


  <div

    v-if="filterMode==='day' && summary"

    class="summary"

    :class="summary.status"

  >


    <div class="summary-icon">

      📊

    </div>



    <div>


      <strong>

        {{summary.totalHours}} часов

      </strong>


      <span>

        за {{filterDate}}

        —

        {{stickerLabel}}

      </span>


    </div>


  </div>








  <!-- Таблица -->


  <div class="card">


    <table>


      <thead>

      <tr>

        <th>
          Дата
        </th>

        <th>
          Часы
        </th>

        <th>
          Задача
        </th>

        <th>
          Сотрудник
        </th>

        <th>
          Описание
        </th>

        <th></th>

      </tr>


      </thead>




      <tbody>


      <tr
        v-for="e in entries"
        :key="e.id"
      >


        <td>

          {{e.entryDate}}

        </td>



        <td>

          <span class="hours">

            {{e.hours}} ч.

          </span>

        </td>



        <td>

          {{taskName(e.taskId)}}

        </td>



        <td>

          {{e.employeeName}}

        </td>



        <td>

          {{e.description || '—'}}

        </td>



        <td>


          <button

            class="delete"

            @click="remove(e.id)"

          >

            🗑

          </button>


        </td>


      </tr>


      </tbody>


    </table>


  </div>









  <!-- Добавление -->


  <div class="card">


    <h2>
      Добавить запись
    </h2>



    <form
      @submit.prevent="create"
    >



      <div class="field">

        <label>
          Дата
        </label>

        <input
          type="date"
          v-model="form.entryDate"
          required
        />

      </div>






      <div class="field">

        <label>
          Часы
        </label>

        <input

          type="number"

          step="0.5"

          min="0.5"

          max="24"

          v-model.number="form.hours"

          required

        />


      </div>






      <div class="field">

        <label>
          Задача
        </label>


        <select

          v-model.number="form.taskId"

          required

        >

          <option disabled value="">

            — выбрать —

          </option>


          <option

            v-for="t in activeTasks"

            :key="t.id"

            :value="t.id"

          >

            {{t.name}}

          </option>


        </select>


      </div>






      <div class="field">

        <label>
          Сотрудник
        </label>


        <input

          v-model="form.employeeName"

          required

        />


      </div>






      <div class="field">

        <label>
          Описание
        </label>


        <input

          v-model="form.description"

        />


      </div>





      <button class="create">

        + Добавить


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
computed,
onMounted
}
from 'vue'


import api from '../services/api'



const emit =
defineEmits(['changed'])




const entries=ref([])

const tasks=ref([])

const error=ref('')

const summary=ref(null)




const filterMode=ref('all')


const filterDate=
ref(new Date().toISOString().slice(0,10))


const filterMonth=
ref(new Date().toISOString().slice(0,7))


const summaryEmployee=ref('')





const form=ref({

entryDate:
new Date().toISOString().slice(0,10),

hours:'',

taskId:'',

employeeName:'',

description:''

})





const activeTasks =
computed(()=>tasks.value.filter(t=>t.isActive))






function taskName(id){

return tasks.value
.find(t=>t.id===id)
?.name ?? '—'

}






const stickerLabel =
computed(()=>{

if(!summary.value)
return ''


return {

yellow:'недостаточно',

green:'норма',

red:'переработка'


}[summary.value.status]


})







async function load(){


let url='/TimeEntries'



if(filterMode.value==='day'){


url+=`?date=${filterDate.value}`


}


else if(filterMode.value==='month'){


const [year,month]=
filterMonth.value.split('-')


url+=
`?year=${year}&month=${month}`


}





const [
entriesRes,
tasksRes
]=await Promise.all([


api.get(url),


api.get('/Tasks')


])



entries.value=
entriesRes.data


tasks.value=
tasksRes.data



if(filterMode.value==='day')

await loadSummary()


}







async function loadSummary(){


if(!summaryEmployee.value)

{

summary.value=null

return

}



const res=
await api.get(
'/TimeEntries/summary',
{

params:{

date:filterDate.value,

employeeName:
summaryEmployee.value

}

})


summary.value=res.data


}







async function create(){


try{


await api.post(
'/TimeEntries',
form.value
)



form.value={

entryDate:
new Date().toISOString().slice(0,10),

hours:'',

taskId:'',

employeeName:
form.value.employeeName,

description:''

}



await load()


emit('changed')


}


catch(e){


error.value=
'Не удалось создать запись'


}



}







async function remove(id){


await api.delete(
`/TimeEntries/${id}`
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






.filters{


display:flex;


gap:20px;


flex-wrap:wrap;


}





.field{


display:flex;


flex-direction:column;


gap:7px;


}





label{


font-size:13px;


color:#64748b;


}





input,
select{


padding:12px;


border:1px solid #ddd;


border-radius:12px;


min-width:180px;


}





table{


width:100%;


border-collapse:collapse;


}





th{


text-align:left;


padding:14px;


font-size:13px;


color:#64748b;


border-bottom:1px solid #eee;


}





td{


padding:15px;


border-bottom:1px solid #eee;


}





.hours{


background:#dbeafe;


color:#1d4ed8;


padding:5px 10px;


border-radius:10px;


font-weight:600;


}






.delete{


background:#fee2e2;


border:none;


color:#dc2626;


padding:8px 12px;


border-radius:10px;


cursor:pointer;


}





.summary{


display:flex;


align-items:center;


gap:15px;


padding:18px;


border-radius:16px;


margin-bottom:20px;


color:white;


font-size:16px;


}





.summary-icon{

font-size:35px;

}



.summary strong{

display:block;

font-size:24px;

}



.summary.green{

background:#22c55e;

}


.summary.yellow{

background:#eab308;

}


.summary.red{

background:#ef4444;

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



.error{


background:#fee2e2;


color:#b91c1c;


padding:12px;


border-radius:12px;


}



</style>