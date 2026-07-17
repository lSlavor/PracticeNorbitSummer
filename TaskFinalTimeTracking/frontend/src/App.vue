<template>
  <div class="app">

    <header class="top-panel">

      <div class="title">

        <div class="logo">
          ⏱
        </div>

        <div>
          <h1>
            Time Tracker
          </h1>

          <p>
            Система учета рабочего времени сотрудников
          </p>
        </div>

      </div>


      <div class="profile">

        👤 Администратор

      </div>


    </header>



    <StatsSummary 
      ref="stats"
    />



    <div class="navigation">


      <button
        v-for="tab in tabs"
        :key="tab.key"

        :class="{
          active: activeTab === tab.key
        }"

        @click="activeTab = tab.key"

      >

        {{tab.icon}}

        {{tab.label}}

      </button>


    </div>




    <section class="content">


      <ProjectsTab
        v-if="activeTab === 'projects'"
        @changed="refreshStats"
      />


      <TasksTab
        v-if="activeTab === 'tasks'"
        @changed="refreshStats"
      />


      <TimeEntriesTab
        v-if="activeTab === 'timeentries'"
        @changed="refreshStats"
      />


    </section>



  </div>
</template>



<script setup>

import {ref} from 'vue'


import StatsSummary from './components/StatsSummary.vue'
import ProjectsTab from './components/ProjectsTab.vue'
import TasksTab from './components/TasksTab.vue'
import TimeEntriesTab from './components/TimeEntriesTab.vue'



const tabs=[

{
key:'projects',
label:'Проекты',
icon:'📁'
},


{
key:'tasks',
label:'Задачи',
icon:'✅'
},


{
key:'timeentries',
label:'Время',
icon:'🕒'
}


]



const activeTab=ref('projects')


const stats=ref(null)



function refreshStats(){

stats.value?.reload()

}


</script>



<style>

*{

box-sizing:border-box;

}



body{

margin:0;

font-family:
Inter,
Segoe UI,
Arial,
sans-serif;

background:#f4f7fb;

color:#1e293b;

}



.app{

max-width:1200px;

margin:auto;

padding:30px;

}



.top-panel{

background:white;

border-radius:22px;

padding:25px 30px;

display:flex;

justify-content:space-between;

align-items:center;

box-shadow:
0 10px 30px rgba(0,0,0,.06);

margin-bottom:25px;

}



.title{

display:flex;

gap:18px;

align-items:center;

}



.logo{

width:55px;

height:55px;

background:#4f46e5;

color:white;

font-size:30px;

display:flex;

align-items:center;

justify-content:center;

border-radius:16px;

}



h1{

margin:0;

font-size:28px;

}



p{

margin:5px 0 0;

color:#64748b;

}



.profile{

background:#f1f5f9;

padding:12px 18px;

border-radius:20px;

font-size:14px;

}




.navigation{

display:flex;

gap:12px;

margin-bottom:25px;

}



.navigation button{

border:none;

background:white;

padding:14px 25px;

border-radius:14px;

font-size:15px;

cursor:pointer;

box-shadow:
0 5px 15px rgba(0,0,0,.05);

transition:.2s;

}



.navigation button:hover{

transform:translateY(-2px);

}



.navigation button.active{

background:#4f46e5;

color:white;

}




.content{

background:white;

border-radius:22px;

padding:25px;

box-shadow:
0 10px 30px rgba(0,0,0,.05);

}



</style>