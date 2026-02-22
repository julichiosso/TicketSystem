<template>
  <div class="min-h-screen text-slate-200 flex font-['Outfit'] select-none transition-colors duration-700">
    <!-- Sidebar Premium -->
    <aside class="w-20 lg:w-64 bg-slate-900/20 border-r border-white/5 flex flex-col transition-all duration-500 backdrop-blur-3xl sticky top-0 h-screen z-40">
      <div class="p-6 flex items-center gap-4">
        <div :class="`w-10 h-10 bg-gradient-to-tr ${settingsStore.themeClasses} rounded-xl flex items-center justify-center shadow-lg active:scale-95 transition-all` ">
          <TicketIcon class="text-white w-6 h-6" />
        </div>
        <span class="text-xl font-black text-white tracking-tighter hidden lg:block uppercase italic">HubTicket</span>
      </div>

      <nav class="flex-1 px-4 py-8 space-y-2">
        <router-link to="/dashboard" class="flex items-center gap-4 px-4 py-3.5 rounded-2xl bg-blue-600/10 text-blue-400 transition-all border border-blue-500/10 group">
          <LayoutDashboardIcon class="w-5 h-5 group-hover:scale-110 transition-transform" />
          <span class="font-bold text-sm hidden lg:block">Panel Principal</span>
        </router-link>
        
        <button @click="showModal = true" class="w-full flex items-center gap-4 px-4 py-3.5 rounded-2xl text-slate-400 hover:text-emerald-400 hover:bg-emerald-500/5 transition-all group">
          <PlusCircleIcon class="w-5 h-5 group-hover:rotate-90 transition-transform duration-500" />
          <span class="font-bold text-sm hidden lg:block">Nuevo Ticket</span>
        </button>
      </nav>

      <div class="p-4 border-t border-slate-800/50">
        <button @click="handleLogout" class="w-full flex items-center gap-4 px-4 py-3.5 rounded-2xl text-slate-500 hover:bg-rose-500/5 hover:text-rose-400 transition-all active:scale-95">
          <LogOutIcon class="w-5 h-5" />
          <span class="font-bold text-sm hidden lg:block">Salir</span>
        </button>
      </div>
    </aside>

    <!-- Main Content -->
    <main class="flex-1 min-w-0 h-screen overflow-y-auto page-fade-in custom-scrollbar">
      <header class="h-20 flex items-center justify-between px-10 bg-white/[0.01] backdrop-blur-xl border-b border-white/[0.02] sticky top-0 z-30">
        <div>
          <h2 class="text-2xl font-black text-white tracking-tight uppercase italic opacity-90">Mis Tickets</h2>
          <p class="text-[10px] text-slate-500 font-bold uppercase tracking-[0.2em]">Soporte y Seguimiento</p>
        </div>
        
        <div class="flex items-center gap-6">
          <button @click="fetchTickets" class="p-2.5 bg-white/[0.03] hover:bg-white/[0.08] text-slate-400 hover:text-white rounded-xl transition-all active:rotate-180 duration-700">
            <RefreshCcwIcon class="w-5 h-5" />
          </button>
          
          <div class="h-8 w-px bg-slate-800"></div>

          <!-- Profile Dropdown Component Placeholderish -->
          <div class="relative profile-menu-container">
            <button @click.stop="showProfileMenu = !showProfileMenu" class="flex items-center gap-4 group cursor-pointer transition-all">
              <div class="text-right hidden sm:block">
                <div class="text-xs font-black text-white uppercase tracking-wider group-hover:text-blue-400 transition-colors">{{ authStore.user?.nombre }}</div>
                <div class="text-[9px] text-slate-500 font-bold uppercase tracking-widest">{{ authStore.user?.rol }}</div>
              </div>
              <div :class="`w-10 h-10 rounded-2xl bg-gradient-to-br from-slate-800 to-slate-900 border border-slate-700 flex items-center justify-center ${settingsStore.accentColor} font-black shadow-xl group-hover:border-blue-500/30 transition-all` ">
                {{ authStore.user?.nombre?.[0] }}
              </div>
            </button>

            <!-- Dropdown Menu -->
            <div v-if="showProfileMenu" class="absolute right-0 mt-4 w-56 bg-slate-900 border border-white/10 rounded-3xl shadow-2xl py-3 z-50 backdrop-blur-3xl animate-in slide-in-from-top-2 duration-300">
              <router-link to="/profile" class="flex items-center gap-3 px-6 py-3 text-slate-400 hover:text-white hover:bg-white/5 transition-all">
                <UserIcon class="w-4 h-4" />
                <span class="text-xs font-bold uppercase tracking-widest">Mi Perfil</span>
              </router-link>
              <router-link to="/settings" class="flex items-center gap-3 px-6 py-3 text-slate-400 hover:text-white hover:bg-white/5 transition-all">
                <SettingsIcon class="w-4 h-4" />
                <span class="text-xs font-bold uppercase tracking-widest">Ajustes</span>
              </router-link>
              <div class="h-px bg-white/5 my-2 mx-6"></div>
              <button @click="handleLogout" class="w-full flex items-center gap-3 px-6 py-3 text-rose-500 hover:bg-rose-500/10 transition-all">
                <LogOutIcon class="w-4 h-4" />
                <span class="text-xs font-bold uppercase tracking-widest">Cerrar Sesión</span>
              </button>
            </div>
          </div>
        </div>
      </header>

      <div class="max-w-[1600px] mx-auto transition-all duration-500" :style="{ padding: 'var(--density-p, 2.5rem)' }">
        <!-- Dashboard Stats -->
        <div class="grid grid-cols-1 md:grid-cols-3 gap-8 mb-16">
          <div class="bg-white/5 border border-white/5 p-8 rounded-[2.5rem] relative overflow-hidden group hover:scale-[1.02] transition-all premium-blur">
            <div class="relative z-10">
              <p class="text-[10px] font-black uppercase tracking-[0.2em] text-white/40 mb-1">Pendientes</p>
              <h4 class="text-4xl font-black text-white mb-2">{{ stats.open }}</h4>
              <p class="text-xs text-slate-500 font-medium italic">En espera de respuesta</p>
            </div>
            <InboxIcon class="absolute -right-6 -bottom-6 w-32 h-32 text-primary/10 group-hover:scale-110 transition-transform duration-700" />
          </div>
          
          <div class="bg-white/5 border border-white/5 p-8 rounded-[2.5rem] relative overflow-hidden group hover:scale-[1.02] transition-all premium-blur">
            <div class="relative z-10">
              <p class="text-[10px] font-black uppercase tracking-[0.2em] text-white/40 mb-1">En Proceso</p>
              <h4 class="text-4xl font-black text-white mb-2">{{ stats.processing }}</h4>
              <p class="text-xs text-slate-500 font-medium italic">Siendo atendidos</p>
            </div>
            <TimerIcon class="absolute -right-6 -bottom-6 w-32 h-32 text-primary/10 group-hover:scale-110 transition-transform duration-700" />
          </div>

          <div class="bg-white/5 border border-white/5 p-8 rounded-[2.5rem] relative overflow-hidden group hover:scale-[1.02] transition-all premium-blur">
            <div class="relative z-10">
              <p class="text-[10px] font-black uppercase tracking-[0.2em] text-white/40 mb-1">Resueltos</p>
              <h4 class="text-4xl font-black text-white mb-2">{{ stats.resolved }}</h4>
              <p class="text-xs text-slate-500 font-medium italic">Soluciones completadas</p>
            </div>
            <CheckCircleIcon class="absolute -right-6 -bottom-6 w-32 h-32 text-emerald-500/10 group-hover:scale-110 transition-transform duration-700" />
          </div>
        </div>

        <!-- Section Title with Quick Add -->
        <div class="flex items-center justify-between mb-8 px-2">
          <div class="flex items-center gap-6">
            <h3 class="text-lg font-black text-white flex items-center gap-3">
               <div :class="`w-1.5 h-6 bg-${settingsStore.themeColor}-600 rounded-full transition-colors` "></div>
               LISTADO DE TICKETS
            </h3>
            <!-- Quick Add Button -->
            <button @click="showModal = true" class="group flex items-center gap-2 bg-white/5 hover:bg-blue-600 text-slate-400 hover:text-white px-4 py-2 rounded-xl border border-white/5 transition-all active:scale-95 shadow-xl shadow-blue-500/0 hover:shadow-blue-500/20">
               <PlusIcon class="w-4 h-4 group-hover:rotate-90 transition-transform" />
               <span class="text-[10px] font-black uppercase tracking-widest hidden sm:inline">Nuevo</span>
            </button>
          </div>
          <div class="text-[10px] text-slate-500 font-bold uppercase tracking-widest bg-white/5 px-4 py-2 rounded-full border border-white/5">
            Total: {{ tickets.length }} tickets
          </div>
        </div>

        <!-- Grid of Cards -->
        <div v-if="tickets.length > 0" class="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 2xl:grid-cols-4 gap-6">
          <TicketCard v-for="ticket in tickets" :key="ticket.id" :ticket="ticket" @click="openTicketDetail(ticket)" class="cursor-pointer" />
        </div>

        <!-- Empty State Premium -->
        <div v-else class="flex flex-col items-center justify-center py-32 border-2 border-dashed border-slate-900 rounded-[3rem] bg-slate-900/10 group transition-all duration-700">
           <div class="w-24 h-24 bg-slate-900 rounded-full flex items-center justify-center mb-8 border border-white/5 group-hover:scale-110 group-hover:border-blue-500/30 transition-all duration-700">
              <InboxIcon class="w-10 h-10 text-slate-700 group-hover:text-blue-500 transition-colors" />
           </div>
           <h3 class="text-xl font-black text-white mb-2 uppercase italic tracking-tight">Tu buzón está vacío</h3>
           <p class="text-slate-500 text-sm max-w-sm text-center mb-10 font-medium leading-relaxed">Parece que no tenés tickets activos actualmente. Si necesitás soporte, creá uno nuevo.</p>
           <button @click="showModal = true" class="flex items-center gap-3 bg-blue-600 hover:bg-blue-500 text-white font-black text-xs uppercase tracking-widest px-10 py-4 rounded-2xl shadow-2xl shadow-blue-500/20 active:scale-95 transition-all">
             Crear mi primer ticket
             <PlusCircleIcon class="w-4 h-4" />
           </button>
        </div>
      </div>
    </main>

    <!-- Create Ticket Modal -->
    <div v-if="showModal" class="fixed inset-0 z-[100] flex items-center justify-center p-6 bg-slate-950/70 backdrop-blur-2xl transition-all duration-500">
      <div class="bg-slate-900/80 border border-white/10 rounded-[3rem] w-full max-w-xl shadow-2xl relative overflow-hidden backdrop-blur-3xl">
        <div class="p-10 relative z-10">
          <div class="flex justify-between items-start mb-10">
            <div>
              <h3 class="text-3xl font-black text-white italic tracking-tighter uppercase line-height-[0.8]">Nuevo Ticket</h3>
              <p class="text-[10px] text-blue-400 font-bold uppercase tracking-[0.3em] mt-1">Soporte HubTicket</p>
            </div>
            <button @click="showModal = false" class="p-2 bg-white/5 hover:bg-white/10 text-slate-400 hover:text-white rounded-full transition-all active:scale-90">
              <XIcon class="w-6 h-6" />
            </button>
          </div>

          <form @submit.prevent="createTicket" class="space-y-8">
            <div class="space-y-4">
              <div class="relative group">
                <label class="absolute -top-2.5 left-4 bg-slate-900 px-2 text-[9px] font-black text-slate-500 uppercase tracking-widest z-10 transition-colors group-focus-within:text-blue-500">Asunto del Problema</label>
                <input v-model="newTicket.title" class="w-full bg-transparent border-2 border-slate-800 text-white px-6 py-4 rounded-2xl focus:border-blue-500 outline-none transition-all font-bold placeholder:text-slate-700" placeholder="Ej: Error al procesar pago" required />
              </div>
              
              <div class="relative group">
                <label class="absolute -top-2.5 left-4 bg-slate-900 px-2 text-[9px] font-black text-slate-500 uppercase tracking-widest z-10 transition-colors group-focus-within:text-blue-500">Descripción Detallada</label>
                <textarea v-model="newTicket.description" rows="4" class="w-full bg-transparent border-2 border-slate-800 text-white px-6 py-4 rounded-2xl focus:border-blue-500 outline-none transition-all font-bold placeholder:text-slate-700" placeholder="¿Qué está fallando exactamente?" required></textarea>
              </div>

              <div class="relative group w-1/2">
                <label class="absolute -top-2.5 left-4 bg-slate-900 px-2 text-[9px] font-black text-slate-500 uppercase tracking-widest z-10 transition-colors group-focus-within:text-blue-500">Nivel de Prioridad</label>
                <select v-model="newTicket.priority" class="w-full bg-transparent border-2 border-slate-800 text-white px-6 py-4 rounded-2xl focus:border-blue-500 outline-none transition-all font-bold appearance-none">
                  <option value="0" class="bg-slate-900">Baja importancia</option>
                  <option value="1" class="bg-slate-900">Moderada</option>
                  <option value="2" class="bg-slate-900">Urgente / Crítica</option>
                </select>
              </div>
            </div>

            <div class="flex gap-4 pt-4">
              <button type="submit" class="w-full bg-gradient-to-r from-blue-600 to-indigo-600 hover:from-blue-500 hover:to-indigo-500 text-white font-black text-sm uppercase tracking-widest py-5 rounded-2xl shadow-xl shadow-blue-500/20 transition-all active:scale-95 flex items-center justify-center gap-3">
                Enviar Solicitud
                <ArrowRightIcon class="w-4 h-4" />
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>

    <!-- Ticket Detail Modal -->
    <TicketDetailModal 
      v-if="selectedTicket" 
      :ticket="selectedTicket" 
      @close="selectedTicket = null" 
    />
  </div>
</template>

<script setup>
import { ref, onMounted, reactive } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';
import { 
  TicketIcon, LayoutDashboardIcon, PlusCircleIcon, LogOutIcon, 
  InboxIcon, TimerIcon, CheckCircleIcon, RefreshCcwIcon, XIcon, ArrowRightIcon,
  PlusIcon, UserIcon, SettingsIcon
} from 'lucide-vue-next';
import { useAuthStore, API_URL } from '../store/auth';
import { useNotificationStore } from '../store/notifications';
import { useSettingsStore } from '../store/settings';
import TicketCard from '../components/TicketCard.vue';
import TicketDetailModal from '../components/TicketDetailModal.vue';

const authStore = useAuthStore();
const notificationStore = useNotificationStore();
const settingsStore = useSettingsStore();
const router = useRouter();

const tickets = ref([]);
const showModal = ref(false);
const showProfileMenu = ref(false);
const selectedTicket = ref(null);
const stats = reactive({ open: 0, processing: 0, resolved: 0 });

const newTicket = reactive({
  title: '',
  description: '',
  priority: '1'
});

const handleLogout = () => {
  authStore.logout();
  router.push('/');
};

const openTicketDetail = (ticket) => {
  selectedTicket.value = ticket;
};

const fetchTickets = async () => {
  try {
    const response = await axios.get(`${API_URL}/tickets/mis-tickets`);
    tickets.value = response.data;
    calculateStats();
  } catch (err) {
    console.error('Error fetching tickets:', err);
    notificationStore.error('No se pudieron cargar tus tickets.');
  }
};

const calculateStats = () => {
  // Backend uses 'Pendiente', 'EnProceso', 'Resuelto' (Spanish)
  stats.open = tickets.value.filter(t => t.estado === 'Pendiente' || t.estado === 0).length;
  stats.processing = tickets.value.filter(t => t.estado === 'EnProceso' || t.estado === 1).length;
  stats.resolved = tickets.value.filter(t => t.estado === 'Resuelto' || t.estado === 2).length;
};

const createTicket = async () => {
  try {
    await axios.post(`${API_URL}/tickets`, {
      titulo: newTicket.title,
      descripcion: newTicket.description,
      prioridad: parseInt(newTicket.priority)
    });
    showModal.value = false;
    newTicket.title = '';
    newTicket.description = '';
    notificationStore.success('¡Ticket enviado correctamente!');
    fetchTickets();
  } catch (err) {
    console.error('Error creating ticket:', err);
    notificationStore.error('Hubo un problema al enviar el ticket.');
  }
};

onMounted(() => {
  if (!authStore.isAuthenticated) {
    router.push('/');
    return;
  }
  fetchTickets();
  
  const closeMenu = (e) => {
    if (!e.target.closest('.profile-menu-container')) {
      showProfileMenu.value = false;
    }
  };
  
  window.addEventListener('click', closeMenu);
});
</script>

<style>
.custom-scrollbar::-webkit-scrollbar {
  width: 6px;
}
.custom-scrollbar::-webkit-scrollbar-track {
  background: transparent;
}
.custom-scrollbar::-webkit-scrollbar-thumb {
  background: rgba(255, 255, 255, 0.05);
  border-radius: 10px;
}
</style>
