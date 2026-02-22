<template>
  <div class="min-h-screen flex items-center justify-center bg-slate-950 px-4">
    <div class="max-w-md w-full">
      <div class="bg-slate-900/50 backdrop-blur-xl border border-slate-800 p-8 rounded-2xl shadow-2xl relative overflow-hidden group">
        <div class="absolute -top-24 -left-24 w-64 h-64 bg-blue-600/10 rounded-full blur-3xl group-hover:bg-blue-600/20 transition-all duration-700"></div>
        
        <div class="relative z-10">
          <div class="flex flex-col items-center mb-10">
            <div class="w-16 h-16 bg-blue-600 rounded-xl flex items-center justify-center mb-4 shadow-lg shadow-blue-600/20">
              <TicketIcon class="text-white w-8 h-8" />
            </div>
            <h1 class="text-2xl font-bold text-white mb-2 font-serif tracking-tight">Crear Cuenta</h1>
            <p class="text-slate-400 text-sm">Unite a la plataforma de soporte premium</p>
          </div>

          <form @submit.prevent="handleRegister" class="space-y-6">
            <div>
              <label class="block text-slate-300 text-xs font-semibold mb-2 uppercase tracking-wider">Nombre Completo</label>
              <input 
                v-model="nombre" 
                type="text" 
                required
                class="w-full bg-slate-800/50 border border-slate-700 text-white px-4 py-3 rounded-xl focus:ring-2 focus:ring-blue-500 outline-none transition-all"
                placeholder="John Doe"
              />
            </div>

            <div>
              <label class="block text-slate-300 text-xs font-semibold mb-2 uppercase tracking-wider">Correo Electrónico</label>
              <input 
                v-model="email" 
                type="email" 
                required
                class="w-full bg-slate-800/50 border border-slate-700 text-white px-4 py-3 rounded-xl focus:ring-2 focus:ring-blue-500 outline-none transition-all"
                placeholder="name@company.com"
              />
            </div>

            <div>
              <label class="block text-slate-300 text-xs font-semibold mb-2 uppercase tracking-wider">Contraseña</label>
              <input 
                v-model="password" 
                type="password" 
                required
                class="w-full bg-slate-800/50 border border-slate-700 text-white px-4 py-3 rounded-xl focus:ring-2 focus:ring-blue-500 outline-none transition-all"
                placeholder="••••••••"
              />
            </div>

            <button 
              type="submit" 
              :disabled="authStore.loading"
              class="w-full bg-blue-600 hover:bg-blue-500 text-white font-semibold py-3 rounded-xl shadow-lg shadow-blue-600/20 transition-all disabled:opacity-50 flex items-center justify-center"
            >
              <span v-if="!authStore.loading">Registrarse Ahora</span>
              <Loader2Icon v-else class="w-5 h-5 animate-spin" />
            </button>
            <ErrorMessage :message="authStore.error" />
          </form>

          <div class="mt-10 pt-6 border-t border-slate-800 text-center">
            <p class="text-slate-400 text-sm">
              ¿Ya tenés una cuenta? 
              <router-link to="/" class="text-blue-400 font-semibold hover:text-blue-300 transition-colors ml-1">Iniciar Sesión</router-link>
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

const nombre = ref('');
const email = ref('');
const password = ref('');

const handleRegister = async () => {
  const success = await authStore.register({
    nombre: nombre.value,
    email: email.value,
    password: password.value,
    rol: 0 // Default to Usuario
  });
  if (success) {
    notificationStore.success('¡Registro exitoso! Iniciando sesión...');
    // Log in automatically or redirect to login
    router.push('/');
  }
};
</script>
