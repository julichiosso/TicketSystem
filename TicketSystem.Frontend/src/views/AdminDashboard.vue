<template>
  <div class="min-h-screen text-slate-200 flex font-['Outfit'] select-none transition-colors duration-700">
    <!-- Sidebar Premium -->
    <aside class="w-20 lg:w-72 bg-slate-900/20 border-r border-white/5 flex flex-col transition-all duration-500 backdrop-blur-3xl sticky top-0 h-screen z-40">
      <div class="p-8 flex items-center gap-4">
        <div :class="`w-12 h-12 bg-gradient-to-br ${settingsStore.themeClasses} rounded-2xl flex items-center justify-center shadow-2xl active:scale-95 transition-all` ">
          <ShieldCheckIcon class="text-white w-7 h-7" />
        </div>
        <div class="hidden lg:block">
          <span class="text-2xl font-black text-white tracking-tighter uppercase italic line-height-none">HubAdmin</span>
          <p class="text-[9px] text-blue-400 font-black tracking-widest uppercase">Management Suite</p>
        </div>
      </div>

      <nav class="flex-1 px-6 py-10 space-y-3">
        <router-link to="/admin" class="flex items-center gap-4 px-5 py-4 rounded-[1.25rem] bg-blue-500/10 text-blue-400 transition-all border border-blue-500/10 group">
          <LayoutDashboardIcon class="w-5 h-5 group-hover:scale-110 transition-transform" />
          <span class="font-bold text-sm hidden lg:block uppercase tracking-wider">Dashboard</span>
        </router-link>
        
        <router-link v-if="authStore.isAdmin" to="/admin" class="flex items-center gap-4 px-5 py-4 rounded-[1.25rem] text-slate-500 hover:text-white hover:bg-white/5 transition-all group">
          <UsersIcon class="w-5 h-5 group-hover:scale-110 transition-transform" />
          <span class="font-bold text-sm hidden lg:block uppercase tracking-wider">Usuarios</span>
        </router-link>
      </nav>

      <div class="p-6 border-t border-white/5">
        <button @click="handleLogout" class="w-full flex items-center gap-4 px-5 py-4 rounded-[1.25rem] text-slate-500 hover:bg-rose-500/5 hover:text-rose-400 transition-all active:scale-95">
          <LogOutIcon class="w-5 h-5" />
          <span class="font-bold text-sm hidden lg:block uppercase tracking-wider">Salir</span>
        </button>
      </div>
    </aside>

    <!-- Main Content -->
    <main class="flex-1 min-w-0 h-screen overflow-y-auto page-fade-in custom-scrollbar">
      <header class="h-24 flex items-center justify-between px-12 bg-white/[0.01] backdrop-blur-2xl border-b border-white/[0.03] sticky top-0 z-30">
        <div class="flex items-center gap-4">
          <div :class="`w-1.5 h-10 bg-${settingsStore.themeColor}-600 rounded-full transition-colors` "></div>
          <div>
            <h2 class="text-2xl font-black text-white tracking-tight uppercase italic opacity-95">Gestión de Tickets</h2>
            <p class="text-[10px] text-slate-500 font-bold uppercase tracking-[0.3em]">Panel de Control Global</p>
          </div>
        </div>
        
        <div class="flex items-center gap-8">
           <!-- Profile Dropdown -->
          <div class="relative profile-menu-container">
            <button @click.stop="showProfileMenu = !showProfileMenu" class="flex items-center gap-5 group cursor-pointer transition-all">
              <div class="text-right hidden sm:block">
                <div class="text-sm font-black text-white uppercase tracking-tight group-hover:text-blue-400 transition-colors">{{ authStore.user?.nombre }}</div>
                <div class="text-[9px] text-blue-500 font-black uppercase tracking-widest bg-blue-500/10 px-2 py-0.5 rounded-md mt-0.5">{{ authStore.user?.rol }}</div>
              </div>
              <div :class="`w-12 h-12 rounded-2xl bg-gradient-to-tr from-slate-800 to-slate-900 border border-white/5 flex items-center justify-center ${settingsStore.accentColor} font-black shadow-2xl group-hover:border-blue-500/30 transition-all group-hover:scale-105` ">
                {{ authStore.user?.nombre?.[0] }}
              </div>
            </button>

            <!-- Dropdown Menu -->
            <div v-if="showProfileMenu" class="absolute right-0 mt-4 w-64 bg-slate-900 border border-white/10 rounded-[2rem] shadow-2xl py-4 z-50 backdrop-blur-3xl animate-in slide-in-from-top-2 duration-300">
              <router-link to="/profile" class="flex items-center gap-4 px-8 py-4 text-slate-400 hover:text-white hover:bg-white/5 transition-all">
                <UserIcon class="w-5 h-5" />
                <span class="text-xs font-black uppercase tracking-[0.2em]">Mi Perfil</span>
              </router-link>
              <router-link to="/settings" class="flex items-center gap-4 px-8 py-4 text-slate-400 hover:text-white hover:bg-white/5 transition-all">
                <SettingsIcon class="w-5 h-5" />
                <span class="text-xs font-black uppercase tracking-[0.2em]">Configuración</span>
              </router-link>
              <div class="h-px bg-white/5 my-3 mx-8"></div>
              <button @click="handleLogout" class="w-full flex items-center gap-4 px-8 py-4 text-rose-500 hover:bg-rose-500/10 transition-all">
                <LogOutIcon class="w-5 h-5" />
                <span class="text-xs font-black uppercase tracking-[0.2em]">Cerrar Sesión</span>
              </button>
            </div>
          </div>
        </div>
      </header>

      <div class="max-w-[1700px] mx-auto transition-all duration-500" :style="{ padding: 'var(--density-p, 2.5rem)' }">
        <!-- Dashboard Stats Summary -->
        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-12">
          <div class="bg-white/5 border border-white/5 p-6 rounded-[2rem] relative overflow-hidden group premium-blur">
            <div class="relative z-10">
              <p class="text-[9px] font-black uppercase tracking-[0.2em] text-indigo-400/60 mb-1">Total Tickets</p>
              <h4 class="text-3xl font-black text-white">{{ stats.total }}</h4>
            </div>
            <TicketIcon class="absolute -right-4 -bottom-4 w-20 h-20 text-indigo-500/5 group-hover:scale-110 transition-transform" />
          </div>

          <div class="bg-white/5 border border-white/5 p-6 rounded-[2rem] relative overflow-hidden group premium-blur">
            <div class="relative z-10">
              <p class="text-[9px] font-black uppercase tracking-[0.2em] text-rose-400/60 mb-1">Urgentes</p>
              <h4 class="text-3xl font-black text-white">{{ stats.urgent }}</h4>
            </div>
            <AlertTriangleIcon class="absolute -right-4 -bottom-4 w-20 h-20 text-rose-500/5 group-hover:scale-110 transition-transform" />
          </div>

          <div class="bg-white/5 border border-white/5 p-6 rounded-[2rem] relative overflow-hidden group premium-blur">
            <div class="relative z-10">
              <p class="text-[9px] font-black uppercase tracking-[0.2em] text-blue-400/60 mb-1">Pendientes</p>
              <h4 class="text-3xl font-black text-white">{{ stats.pending }}</h4>
            </div>
            <InboxIcon class="absolute -right-4 -bottom-4 w-20 h-20 text-blue-500/5 group-hover:scale-110 transition-transform" />
          </div>

          <div class="bg-white/5 border border-white/5 p-6 rounded-[2rem] relative overflow-hidden group premium-blur transition-all hover:scale-[1.02]">
            <div class="relative z-10">
              <p class="text-[9px] font-black uppercase tracking-[0.2em] text-white/40 mb-1">En Proceso</p>
              <h4 class="text-3xl font-black text-white">{{ stats.processing }}</h4>
            </div>
            <TimerIcon class="absolute -right-4 -bottom-4 w-20 h-20 text-white/5 group-hover:scale-110 transition-transform" />
          </div>
        </div>

        <!-- Dashboard Filters & Search Premium -->
        <div class="grid grid-cols-1 xl:grid-cols-4 gap-6 mb-12">
          <div class="xl:col-span-2 relative group">
            <SearchIcon class="absolute left-5 top-1/2 -translate-y-1/2 w-5 h-5 text-slate-500 group-focus-within:text-blue-400 transition-colors" />
            <input v-model="filters.busqueda" class="w-full h-16 bg-white/[0.02] border border-white/5 rounded-3xl pl-14 pr-6 text-white font-bold outline-none focus:border-blue-500/50 focus:bg-white/[0.04] transition-all placeholder:text-slate-600" placeholder="Buscar por ID o asunto..." />
          </div>
          
          <div class="relative group">
            <select v-model="filters.estado" class="w-full h-16 bg-white/[0.02] border border-white/5 rounded-3xl px-6 text-white font-bold outline-none focus:border-blue-500/50 focus:bg-white/[0.04] transition-all appearance-none cursor-pointer">
              <option :value="null">Todos los Estados</option>
              <option value="0">Abierto</option>
              <option value="1">En Progreso</option>
              <option value="2">Resuelto</option>
              <option value="3">Cerrado</option>
            </select>
            <ChevronDownIcon class="absolute right-6 top-1/2 -translate-y-1/2 w-4 h-4 text-slate-500 pointer-events-none" />
          </div>

          <button @click="fetchTickets" class="h-16 bg-blue-600 hover:bg-blue-500 text-white font-black text-xs uppercase tracking-[0.2em] rounded-3xl shadow-2xl shadow-blue-500/20 transition-all active:scale-95 flex items-center justify-center gap-3">
             <RefreshCwIcon class="w-5 h-5" />
             Actualizar
          </button>
        </div>

        <!-- Section Title with Quick Add for Admins too -->
        <div class="flex items-center justify-between mb-8 px-2">
          <h3 class="text-lg font-black text-white flex items-center gap-3">
             <div :class="`w-1.5 h-6 bg-${settingsStore.themeColor}-600 rounded-full transition-colors` "></div>
             TICKETS DEL SISTEMA
          </h3>
          <div class="text-[10px] text-slate-500 font-bold uppercase tracking-widest bg-white/5 px-4 py-2 rounded-full border border-white/5">
            Total: {{ tickets.length }} tickets
          </div>
        </div>

        <!-- Content Area -->
        <div class="space-y-4">
          <div v-for="ticket in tickets" :key="ticket.id" @click="openTicketDetail(ticket)" class="cursor-pointer group relative premium-blur rounded-[2.5rem] p-8 flex flex-col xl:flex-row xl:items-center justify-between gap-8 transition-all duration-500 hover:shadow-2xl hover:shadow-primary/5 hover:translate-y-[-4px]">
            <div class="flex items-center gap-8">
              <div class="w-16 h-16 bg-slate-950 rounded-2xl flex items-center justify-center text-xs font-mono text-slate-600 border border-white/5 group-hover:border-blue-500/20 transition-all">
                #{{ ticket.id?.substring(0, 5) }}
              </div>
              <div>
                <div class="flex items-center gap-3 mb-1.5">
                  <h3 class="text-xl font-black text-white tracking-tight group-hover:text-blue-400 transition-colors uppercase italic">{{ ticket.titulo }}</h3>
                  <StatusBadge :type="ticket.prioridad" :label="ticket.prioridad" />
                </div>
                <div class="flex items-center gap-4 text-xs font-bold text-slate-500 uppercase tracking-widest">
                  <span class="flex items-center gap-1.5 text-blue-400/80"><UserIcon class="w-3.5 h-3.5" /> {{ ticket.usuarioNombre }}</span>
                  <span>•</span>
                  <span>{{ new Date(ticket.fechaCreacion).toLocaleString() }}</span>
                </div>
              </div>
            </div>

            <div class="flex flex-col sm:flex-row items-center gap-8 xl:gap-12" @click.stop>
              <div class="w-full sm:w-auto">
                <StatusBadge :type="ticket.estado" :label="ticket.estado" class="scale-125 origin-center" />
              </div>

              <div class="flex items-center gap-3 w-full sm:w-auto">
                <button v-if="ticket.estado !== 'Resolved' && ticket.estado !== 2" @click="updateStatus(ticket.id, 1)" class="flex-1 sm:flex-none h-12 px-6 bg-indigo-600/10 hover:bg-indigo-600 text-indigo-400 hover:text-white font-black text-[10px] uppercase tracking-widest rounded-xl transition-all active:scale-95 border border-indigo-600/20">
                  Procesar
                </button>
                <button v-if="ticket.estado !== 'Resolved' && ticket.estado !== 2" @click="updateStatus(ticket.id, 2)" class="flex-1 sm:flex-none h-12 px-6 bg-emerald-600/10 hover:bg-emerald-600 text-emerald-400 hover:text-white font-black text-[10px] uppercase tracking-widest rounded-xl transition-all active:scale-95 border border-emerald-600/20">
                  Resolver
                </button>
                <button @click="deleteTicket(ticket.id)" class="p-3 bg-rose-500/5 hover:bg-rose-500/20 text-rose-500 rounded-xl transition-all border border-rose-500/10">
                   <Trash2Icon class="w-5 h-5" />
                </button>
              </div>
            </div>

            <!-- Absolute decorative number -->
            <div class="absolute right-10 top-1/2 -translate-y-1/2 text-[120px] font-black italic text-white/[0.01] pointer-events-none group-hover:text-white/[0.02] transition-colors">
              0{{ tickets.indexOf(ticket) + 1 }}
            </div>
          </div>
        </div>
      </div>
    </main>

    <!-- Ticket Detail Modal -->
    <TicketDetailModal 
      v-if="selectedTicket" 
      :ticket="selectedTicket" 
      @close="selectedTicket = null" 
    />
  </div>
</template>

<script setup>
import { ref, reactive, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';
import { 
  TicketIcon, LayoutDashboardIcon, UsersIcon, LogOutIcon, 
  ShieldCheckIcon, SearchIcon, AlertTriangleIcon, TimerIcon, 
  TrendingUpIcon, RefreshCwIcon, UserIcon, CheckCircleIcon,
  ChevronDownIcon, Trash2Icon, SettingsIcon, InboxIcon
} from 'lucide-vue-next';
import { useAuthStore, API_URL } from '../store/auth';
import { useNotificationStore } from '../store/notifications';
import { useSettingsStore } from '../store/settings';
import StatusBadge from '../components/StatusBadge.vue';
import TicketDetailModal from '../components/TicketDetailModal.vue';

const authStore = useAuthStore();
const notificationStore = useNotificationStore();
const settingsStore = useSettingsStore();
const router = useRouter();

const tickets = ref([]);
const showProfileMenu = ref(false);
const selectedTicket = ref(null);
const stats = reactive({ total: 0, urgent: 0, pending: 0, processing: 0 });
const filters = reactive({
  busqueda: '',
  estado: null
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
    const params = {
      Page: 1,
      PageSize: 100
    };
    if (filters.busqueda) params.Titulo = filters.busqueda;
    if (filters.estado !== null) params.Estado = filters.estado;

    const response = await axios.get(`${API_URL}/tickets`, { params });
    // Root structure: { success: true, data: { data: [...], totalRecords: X } }
    const result = response.data.data;
    tickets.value = (result && result.data) ? result.data : (Array.isArray(result) ? result : []);
    stats.total = (result && result.totalRecords) ? result.totalRecords : tickets.value.length;
    
    // Sync with backend Spanish enums
    stats.urgent = tickets.value.filter(t => t.prioridad === 'Alta' || t.prioridad === 2).length;
    stats.pending = tickets.value.filter(t => t.estado === 'Pendiente' || t.estado === 0).length;
    stats.processing = tickets.value.filter(t => t.estado === 'EnProceso' || t.estado === 1).length;
  } catch (err) {
    console.error('Error fetching admin tickets:', err);
    notificationStore.error('Error al sincronizar los tickets del sistema.');
  }
};

const updateStatus = async (ticketId, newStatus) => {
  try {
    await axios.patch(`${API_URL}/tickets/${ticketId}/estado`, newStatus, {
      headers: { 'Content-Type': 'application/json' }
    });
    notificationStore.success('Estado del ticket actualizado con éxito.');
    fetchTickets();
  } catch (err) {
    console.error('Error updating status:', err);
    notificationStore.error('No se pudo modificar el estado del ticket.');
  }
};

const deleteTicket = async (id) => {
  if (!confirm('¿Estás seguro de que querés eliminar este ticket de forma permanente?')) return;
  try {
    await axios.delete(`${API_URL}/tickets/${id}`);
    notificationStore.info('Ticket eliminado del sistema.');
    fetchTickets();
  } catch (err) {
    notificationStore.error('Error al intentar eliminar el ticket.');
  }
};

onMounted(() => {
  if (!authStore.isAdmin && !authStore.isOperador) {
    router.push('/dashboard');
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
.custom-scrollbar::-webkit-scrollbar-track { background: transparent; }
.custom-scrollbar::-webkit-scrollbar-thumb {
  background: rgba(255, 255, 255, 0.05);
  border-radius: 10px;
}
</style>
