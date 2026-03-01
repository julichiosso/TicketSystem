<template>
 <aside 
 class="bg-slate-50/40 border-r border-slate-200 flex flex-col transition-all duration-500 sticky top-0 h-screen z-50 group shadow-md overflow-hidden"
 :class="isExpanded ? 'w-64' : 'w-20'"
 @mouseenter="isExpanded = true"
 @mouseleave="isExpanded = false"
    <!-- Logo Section -->
    <div class="flex items-center h-24 flex-shrink-0">
      <div class="w-20 flex-shrink-0 flex items-center justify-center">
        <div :class="`w-10 h-10 bg-gradient-to-tr ${settingsStore.themeClasses} rounded-2xl flex items-center justify-center shadow-md active:scale-95 transition-all` ">
          <TicketIcon class="text-white w-5 h-5" />
        </div>
      </div>
      <div 
        class="transition-all duration-500 overflow-hidden whitespace-nowrap"
        :class="isExpanded ? 'opacity-100 translate-x-0 w-auto' : 'opacity-0 -translate-x-4 w-0'"
      >
        <span class="text-xl font-bold text-slate-900 tracking-tight">TicketSystem</span>
      </div>
    </div>

    <!-- Navigation -->
    <nav class="flex-1 py-4 space-y-2 overflow-y-auto custom-scrollbar overflow-x-hidden">
      <div v-for="item in navItems" :key="item.to">
        <router-link 
          v-if="item.show"
          :to="item.to" 
          class="flex items-center py-3 mx-2 rounded-xl transition-all duration-300 group/link relative overflow-hidden"
          :class="isActive(item.to) ? 'bg-blue-600/10 text-blue-600 font-medium' : 'text-slate-500 hover:text-slate-900 mx-0 hover:mx-2 hover:bg-slate-100/50'"
        >
          <!-- Fixed width icon container for perfect centering -->
          <div class="w-16 flex-shrink-0 flex items-center justify-center">
            <component :is="item.icon" class="w-5 h-5 transition-transform duration-500 group-hover/link:scale-110" />
          </div>
          <span 
            class="text-sm tracking-wide transition-all duration-500 whitespace-nowrap"
            :class="isExpanded ? 'opacity-100 translate-x-0' : 'opacity-0 -translate-x-4 w-0'"
          >
            {{ item.label }}
          </span>

          <!-- Active Indicator dot (Left bar instead of dot for cleaner look and scaling) -->
          <div v-if="isActive(item.to)" class="absolute left-0 top-1/2 -translate-y-1/2 w-1 h-6 rounded-r-md bg-blue-600 shadow-sm shadow-blue-200"></div>
        </router-link>
      </div>
    </nav>

    <!-- User Section -->
    <div class="border-t border-slate-200 py-4 pb-6 flex-shrink-0 flex flex-col gap-2">
      <router-link 
        to="/profile"
        class="flex items-center py-2 transition-all duration-300 relative group/link mx-2 rounded-xl"
        :class="isActive('/profile') ? 'bg-slate-100' : 'hover:bg-slate-100/50 text-slate-500 hover:text-slate-900'"
      >
        <div class="w-16 flex-shrink-0 flex items-center justify-center">
          <div class="w-10 h-10 rounded-xl bg-white border border-slate-200 flex items-center justify-center overflow-hidden group-hover/link:border-blue-500/30 transition-all shadow-sm">
            <div v-if="authStore.user?.avatar" class="w-full h-full bg-cover" :style="`background-image: url(${authStore.user.avatar})`"></div>
            <span v-else class="text-xs font-bold text-blue-600">{{ authStore.user?.nombre?.[0] }}</span>
          </div>
        </div>
        <div 
          class="flex flex-col overflow-hidden transition-all duration-500 whitespace-nowrap"
          :class="isExpanded ? 'opacity-100 translate-x-0 w-auto' : 'opacity-0 -translate-x-4 w-0'"
        >
          <span class="text-xs font-bold text-slate-900 truncate">{{ authStore.user?.nombre }}</span>
          <span class="text-[10px] text-slate-500 font-medium truncate">{{ authStore.user?.rol }}</span>
        </div>
      </router-link>

      <button 
        @click="handleLogout" 
        class="w-full flex items-center py-3 text-slate-500 hover:text-rose-600 transition-all duration-300 active:scale-95 group/logout"
      >
        <div class="w-20 flex-shrink-0 flex items-center justify-center">
          <LogOutIcon class="w-5 h-5 transition-transform group-hover/logout:-translate-x-1" />
        </div>
        <span 
          class="font-medium text-sm transition-all duration-500 whitespace-nowrap"
          :class="isExpanded ? 'opacity-100 translate-x-0 w-auto' : 'opacity-0 -translate-x-4 w-0'"
        >
          Cerrar Sesión
        </span>
      </button>
    </div>
 </aside>
</template>

<script setup>
import { ref, computed } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { 
 TicketIcon, 
 LayoutDashboardIcon, 
 SettingsIcon, 
 LogOutIcon, 
 UserIcon,
 ShieldCheckIcon,
 HardHatIcon
} from 'lucide-vue-next';
import { useAuthStore } from '../store/auth';
import { useSettingsStore } from '../store/settings';

const route = useRoute();
const router = useRouter();
const authStore = useAuthStore();
const settingsStore = useSettingsStore();

const isExpanded = ref(false);

const navItems = computed(() => [
 { 
 label: 'Panel Principal', 
 to: '/dashboard', 
 icon: LayoutDashboardIcon, 
 show: true 
 },
 { 
 label: 'Admin Suite', 
 to: '/admin', 
 icon: ShieldCheckIcon, 
 show: authStore.isAdmin 
 },
 { 
 label: 'Operador', 
 to: '/operator', 
 icon: HardHatIcon, 
 show: authStore.isOperador 
 },
 { 
 label: 'Mi Perfil', 
 to: '/profile', 
 icon: UserIcon, 
 show: true 
 },
 { 
 label: 'Ajustes', 
 to: '/settings', 
 icon: SettingsIcon, 
 show: true 
 },
]);

const isActive = (path) => route.path === path;

const handleLogout = () => {
 authStore.logout();
 router.push('/');
};
</script>

<style scoped>
/* Scrollbar now relies on global style.css */
</style>
