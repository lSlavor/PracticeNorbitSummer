<template>

<div 
  class="stats-grid"
  v-if="!loading"
>


  <div class="stat-card blue">

    <div class="icon">
      📁
    </div>

    <div>

      <div class="value">
        {{stats.totalProjects}}
      </div>

      <div class="label">
        Всего проектов
      </div>

    </div>

  </div>




  <div class="stat-card green">

    <div class="icon">
      ✅
    </div>

    <div>

      <div class="value">
        {{stats.activeProjects}}
      </div>

      <div class="label">
        Активных проектов
      </div>

    </div>

  </div>




  <div class="stat-card purple">

    <div class="icon">
      📝
    </div>

    <div>

      <div class="value">
        {{stats.activeTasks}}
      </div>

      <div class="label">
        Активных задач
      </div>

    </div>

  </div>





  <div class="stat-card orange">

    <div class="icon">
      🕒
    </div>


    <div>

      <div class="value">
        {{stats.entriesToday}}
      </div>


      <div class="label">
        Записей сегодня
      </div>

    </div>

  </div>





  <div class="stat-card red">

    <div class="icon">
      ⏱
    </div>


    <div>

      <div class="value">
        {{stats.hoursThisMonth}}
      </div>


      <div class="label">
        Часов за месяц
      </div>


    </div>


  </div>



</div>


</template>




<script setup>

import {ref, reactive, onMounted} from 'vue'

import api from '../services/api'



const loading=ref(true)



const stats=reactive({

totalProjects:0,

activeProjects:0,

activeTasks:0,

entriesToday:0,

hoursThisMonth:0

})




async function load(){


const today=new Date()


const todayStr=
today.toISOString().slice(0,10)



const year=today.getFullYear()

const month=today.getMonth()+1



const [
projectsRes,
tasksRes,
todayEntriesRes,
monthEntriesRes

]=await Promise.all([


api.get('/Projects'),


api.get('/Tasks'),


api.get('/TimeEntries',
{
params:{
date:todayStr
}
}),


api.get('/TimeEntries',
{
params:{
year,
month
}
})


])



stats.totalProjects=
projectsRes.data.length



stats.activeProjects=
projectsRes.data
.filter(p=>p.isActive)
.length



stats.activeTasks=
tasksRes.data
.filter(t=>t.isActive)
.length



stats.entriesToday=
todayEntriesRes.data.length



stats.hoursThisMonth=
monthEntriesRes.data
.reduce(
(sum,e)=>sum+e.hours,
0
)



loading.value=false


}




defineExpose({
reload:load
})



onMounted(load)


</script>





<style scoped>


.stats-grid{


display:grid;


grid-template-columns:
repeat(auto-fit,minmax(220px,1fr));


gap:18px;


margin-bottom:25px;


}





.stat-card{


height:120px;


border-radius:20px;


padding:22px;


display:flex;


align-items:center;


gap:18px;


color:white;


box-shadow:
0 12px 25px rgba(0,0,0,.12);


position:relative;


overflow:hidden;


}




.stat-card::after{


content:"";


position:absolute;


width:130px;


height:130px;


right:-40px;


bottom:-40px;


background:
rgba(255,255,255,.15);


border-radius:50%;


}






.icon{


font-size:38px;


width:55px;


height:55px;


display:flex;


align-items:center;


justify-content:center;


background:
rgba(255,255,255,.2);


border-radius:15px;


}




.value{


font-size:34px;


font-weight:800;


line-height:1;


}




.label{


margin-top:8px;


font-size:14px;


opacity:.9;


}





.blue{


background:
linear-gradient(
135deg,
#3b82f6,
#2563eb
);

}



.green{


background:
linear-gradient(
135deg,
#22c55e,
#15803d
);

}



.purple{


background:
linear-gradient(
135deg,
#a855f7,
#7e22ce
);

}



.orange{


background:
linear-gradient(
135deg,
#f59e0b,
#d97706
);

}



.red{


background:
linear-gradient(
135deg,
#ef4444,
#b91c1c
);

}



</style>