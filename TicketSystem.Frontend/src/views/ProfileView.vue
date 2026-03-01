<template>
 <div class="min-h-screen text-slate-900 flex font-sans select-none transition-colors duration-700">

 <!-- Sidebar -->
 <aside class="w-20 lg:w-64 bg-white border-r border-slate-200 flex flex-col transition-all duration-500 sticky top-0 h-screen z-40">
 <div class="p-6 flex items-center gap-4">
 <div :class="`w-10 h-10 bg-gradient-to-tr ${settingsStore.themeClasses} rounded-xl flex items-center justify-center shadow-lg`">
 <UserIcon class="text-slate-900 w-6 h-6" />
 </div>
 <span class="text-xl font-black text-slate-900 tracking-tighter hidden lg:block uppercase italic">Mi Perfil</span>
 </div>

 <nav class="flex-1 px-4 py-8 space-y-2">
 <router-link :to="authStore.isOperador ? '/admin' : '/dashboard'" class="flex items-center gap-4 px-4 py-3.5 rounded-2xl text-slate-500 hover:text-slate-900 hover:bg-slate-50 transition-all group">
 <LayoutDashboardIcon class="w-5 h-5 group-hover:scale-110 transition-transform" />
 <span class="font-bold text-sm hidden lg:block">Dashboard</span>
 </router-link>
 <router-link to="/settings" class="flex items-center gap-4 px-4 py-3.5 rounded-2xl text-slate-500 hover:text-slate-900 hover:bg-slate-50 transition-all group">
 <SettingsIcon class="w-5 h-5 group-hover:scale-110 transition-transform" />
 <span class="font-bold text-sm hidden lg:block">Configuración</span>
 </router-link>
 </nav>

 <div class="p-4 border-t border-slate-200">
 <button @click="handleLogout" class="w-full flex items-center gap-4 px-4 py-3.5 rounded-2xl text-slate-500 hover:bg-rose-500/5 hover:text-rose-400 transition-all active:scale-95">
 <LogOutIcon class="w-5 h-5" />
 <span class="font-bold text-sm hidden lg:block">Salir</span>
 </button>
 </div>
 </aside>

 <!-- Main -->
 <main class="flex-1 min-w-0 h-screen overflow-y-auto page-fade-in custom-scrollbar">
 <header class="h-20 flex items-center px-10 bg-white/[0.01] border-b border-white/[0.02] sticky top-0 z-30">
 <h2 class="text-2xl font-black text-slate-900 tracking-tight uppercase italic opacity-90">Mi Perfil</h2>
 </header>

 <div class="max-w-3xl mx-auto py-16 px-10 space-y-8 page-fade-in">

 <!-- Avatar Card -->
 <div class="bg-white border border-slate-200 shadow-sm rounded-[2.5rem] p-10 flex flex-col sm:flex-row items-center gap-10">
 <!-- Big Avatar -->
 <div :class="`relative w-32 h-32 rounded-3xl bg-gradient-to-br ${settingsStore.themeClasses} flex items-center justify-center text-5xl font-black text-slate-900 shadow-md select-none`">
 {{ authStore.user?.nombre?.[0]?.toUpperCase() }}
 <!-- Online dot -->
 <div class="absolute -bottom-2 -right-2 w-6 h-6 bg-emerald-500 rounded-full border-4 border-slate-900"></div>
 </div>

 <div class="text-center sm:text-left">
 <h1 class="text-4xl font-black text-slate-900 tracking-tighter">{{ authStore.user?.nombre }}</h1>
 <p class="text-slate-500 text-sm mt-1">{{ authStore.user?.email }}</p>
 <div class="mt-4 flex gap-3 justify-center sm:justify-start flex-wrap">
 <span class="text-xs font-black uppercase tracking-widest bg-primary/10 text-primary border border-primary/20 px-4 py-1.5 rounded-full">
 {{ authStore.user?.rol }}
 </span>
 <span class="text-xs font-black uppercase tracking-widest bg-emerald-500/10 text-emerald-400 border border-emerald-500/20 px-4 py-1.5 rounded-full">
 Activo
 </span>
 </div>
 </div>
 </div>

 <!-- Info Cards -->
 <div class="grid grid-cols-1 sm:grid-cols-2 gap-6">
 <div class="bg-white border border-slate-200 shadow-sm rounded-3xl p-8">
 <h3 class="text-[10px] font-black text-slate-500 uppercase tracking-widest mb-4">Información Personal</h3>
 <div class="space-y-4">
 <div>
 <p class="text-[9px] text-slate-600 font-bold uppercase tracking-widest mb-1">Nombre</p>
 <p class="text-slate-900 font-bold">{{ authStore.user?.nombre }}</p>
 </div>
 <div>
 <p class="text-[9px] text-slate-600 font-bold uppercase tracking-widest mb-1">Email</p>
 <p class="text-slate-900 font-bold">{{ authStore.user?.email }}</p>
 </div>
 <div>
 <p class="text-[9px] text-slate-600 font-bold uppercase tracking-widest mb-1">Rol</p>
 <p class="text-slate-900 font-bold">{{ authStore.user?.rol }}</p>
 </div>
 </div>
 </div>

 <div class="bg-white border border-slate-200 shadow-sm rounded-3xl p-8">
 <h3 class="text-[10px] font-black text-slate-500 uppercase tracking-widest mb-4">Acceso Rápido</h3>
 <div class="space-y-3">
 <router-link 
 :to="authStore.isOperador ? '/admin' : '/dashboard'"
 class="flex items-center gap-4 p-4 rounded-2xl bg-white/5 hover:bg-slate-50 transition-all group"
 >
 <LayoutDashboardIcon class="w-5 h-5 text-primary group-hover:scale-110 transition-transform" />
 <span class="text-sm font-bold text-slate-900">Ir al Dashboard</span>
 </router-link>
 <router-link to="/settings" class="flex items-center gap-4 p-4 rounded-2xl bg-white/5 hover:bg-slate-50 transition-all group">
 <SettingsIcon class="w-5 h-5 text-primary group-hover:scale-110 transition-transform" />
 <span class="text-sm font-bold text-slate-900">Configuración del Sistema</span>
 </router-link>
 </div>
 </div>
 </div>

 <!-- Logout button -->
 <button @click="handleLogout" class="w-full py-5 bg-rose-500/5 hover:bg-rose-500/10 text-rose-400 font-black text-sm uppercase tracking-widest rounded-3xl border border-rose-500/10 transition-all flex items-center justify-center gap-3">
 <LogOutIcon class="w-5 h-5" />
 Cerrar Sesión
 </button>

 </div>
 </main>
 </div>
</template>

<script setup>
import { useRouter } from 'vue-router';
import { UserIcon, LayoutDashboardIcon, SettingsIcon, LogOutIcon } from 'lucide-vue-next';
import { useAuthStore } from '../store/auth';
import { useSettingsStore } from '../store/settings';

const authStore = useAuthStore();
const settingsStore = useSettingsStore();
const router = useRouter();

const handleLogout = () => {
 authStore.logout();
 router.push('/');
};
</script>
