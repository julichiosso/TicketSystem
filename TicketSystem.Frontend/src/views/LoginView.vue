<template>
  <div class="min-h-screen flex items-center justify-center bg-slate-950 px-4">
    <div class="max-w-md w-full">
      <!-- Main Glassmorphism Card -->
      <div class="bg-slate-900/50 backdrop-blur-xl border border-slate-800 p-8 rounded-2xl shadow-2xl relative overflow-hidden group">
        <!-- Abstract background glow -->
        <div class="absolute -top-24 -left-24 w-64 h-64 bg-blue-600/10 rounded-full blur-3xl group-hover:bg-blue-600/20 transition-all duration-700"></div>
        <div class="absolute -bottom-24 -right-24 w-64 h-64 bg-slate-600/10 rounded-full blur-3xl group-hover:bg-slate-600/20 transition-all duration-700"></div>

        <div class="relative z-10">
          <div class="flex flex-col items-center mb-10">
            <div class="w-16 h-16 bg-blue-600 rounded-xl flex items-center justify-center mb-4 shadow-lg shadow-blue-600/20">
              <TicketIcon class="text-white w-8 h-8" />
            </div>
            <h1 class="text-2xl font-bold text-white mb-2 font-serif tracking-tight">TicketSystem</h1>
            <p class="text-slate-400 text-sm">Experimentá el soporte premium</p>
          </div>

          <form @submit.prevent="handleLogin" class="space-y-6">
            <div>
              <label class="block text-slate-300 text-xs font-semibold mb-2 uppercase tracking-wider">Correo Electrónico</label>
              <div class="relative">
                <input 
                  v-model="email" 
                  type="email" 
                  required
                  class="w-full bg-slate-800/50 border border-slate-700 text-white px-4 py-3 rounded-xl focus:ring-2 focus:ring-blue-500 focus:border-transparent transition-all outline-none placeholder:text-slate-500"
                  placeholder="name@company.com"
                />
              </div>
            </div>

            <div>
              <div class="flex justify-between items-center mb-2">
                <label class="block text-slate-300 text-xs font-semibold uppercase tracking-wider">Contraseña</label>
                <a href="#" class="text-blue-400 text-xs hover:text-blue-300 transition-colors">¿Olvidaste tu contraseña?</a>
              </div>
              <input 
                v-model="password" 
                type="password" 
                required
                class="w-full bg-slate-800/50 border border-slate-700 text-white px-4 py-3 rounded-xl focus:ring-2 focus:ring-blue-500 focus:border-transparent transition-all outline-none placeholder:text-slate-500"
                placeholder="••••••••"
              />
            </div>

            <div class="flex items-center">
              <input type="checkbox" id="remember" class="w-4 h-4 bg-slate-800 border-slate-700 rounded text-blue-600 focus:ring-offset-slate-900 focus:ring-blue-500" />
              <label for="remember" class="ml-2 text-slate-400 text-sm cursor-pointer select-none">Recordarme por 30 días</label>
            </div>

            <button 
              type="submit" 
              :disabled="authStore.loading"
              class="w-full bg-blue-600 hover:bg-blue-500 text-white font-semibold py-3 rounded-xl shadow-lg shadow-blue-600/20 transition-all active:scale-[0.98] disabled:opacity-50 disabled:cursor-not-allowed flex items-center justify-center"
            >
              <span v-if="!authStore.loading">Iniciar Sesión</span>
              <Loader2Icon v-else class="w-5 h-5 animate-spin" />
            </button>
            <ErrorMessage :message="authStore.error" />
          </form>

          <div class="mt-10 pt-6 border-t border-slate-800 text-center">
            <p class="text-slate-400 text-sm">
              ¿Nuevo en la plataforma? 
              <router-link to="/register" class="text-blue-400 font-semibold hover:text-blue-300 transition-colors ml-1">Crear una cuenta</router-link>
            </p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useAuthStore } from '../store/auth';
import { useNotificationStore } from '../store/notifications';
import { TicketIcon, Loader2Icon } from 'lucide-vue-next';
import ErrorMessage from '../components/ErrorMessage.vue';

const authStore = useAuthStore();
const notificationStore = useNotificationStore();
const router = useRouter();

const email = ref('');
const password = ref('');

const handleLogin = async () => {
  const success = await authStore.login(email.value, password.value);
  if (success) {
    notificationStore.success(`¡Bienvenido de nuevo, ${authStore.user?.nombre}!`);
    if (authStore.isAdmin || authStore.isOperador) {
      router.push('/admin');
    } else {
      router.push('/dashboard');
    }
  }
};
</script>

<style scoped>
/* Glassmorphism focus */
input:focus {
  background-color: rgba(30, 41, 59, 0.7);
}
</style>
