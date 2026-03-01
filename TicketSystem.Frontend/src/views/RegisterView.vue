<template>
  <div class="min-h-screen flex items-center justify-center bg-slate-50 relative overflow-hidden px-4">
    <!-- Premium background atmospheric blobs -->
    <div class="absolute top-0 left-0 w-[500px] h-[500px] bg-blue-600/5 rounded-full blur-3xl -translate-x-1/2 -translate-y-1/2"></div>
    <div class="absolute bottom-0 right-0 w-[600px] h-[600px] bg-indigo-600/5 rounded-full blur-3xl translate-x-1/3 translate-y-1/3"></div>

    <div class="max-w-md w-full relative z-10">
      <div class="bg-white/90 backdrop-blur-xl border border-slate-200/60 p-8 rounded-[2rem] shadow-sm relative overflow-hidden group">
        
        <!-- Inner card decorative blobs -->
        <div class="absolute -top-32 -left-32 w-80 h-80 bg-blue-400/10 rounded-full blur-3xl group-hover:bg-blue-400/20 transition-all duration-700 mix-blend-multiply"></div>
        <div class="absolute -bottom-32 -right-32 w-80 h-80 bg-indigo-400/10 rounded-full blur-3xl group-hover:bg-indigo-400/20 transition-all duration-700 mix-blend-multiply"></div>

        <div class="relative z-10">
          <div class="flex flex-col items-center mb-10">
            <div class="w-16 h-16 bg-blue-600 rounded-2xl flex items-center justify-center mb-4 shadow-sm shadow-blue-600/20">
              <TicketIcon class="text-white w-8 h-8" />
            </div>
            <h1 class="text-3xl font-extrabold text-slate-900 mb-2 tracking-tight">Crear Cuenta</h1>
            <p class="text-slate-500 text-sm font-medium">Unite a la plataforma de soporte premium</p>
          </div>

          <form @submit.prevent="handleRegister" class="space-y-6">
            <div>
              <label class="block text-slate-400 text-xs font-bold mb-2 uppercase tracking-wide">Nombre Completo</label>
              <input 
                v-model="nombre" 
                type="text" 
                @input="formErrors.nombre = ''"
                class="w-full bg-slate-50/50 border border-slate-200/80 text-slate-900 px-4 py-3.5 rounded-xl focus:ring-2 focus:ring-blue-500 outline-none transition-all placeholder:text-slate-400 font-medium"
                placeholder="John Doe"
              />
              <p v-if="formErrors.nombre" class="text-rose-500 text-xs font-medium mt-1.5 flex items-center gap-1">
                <AlertCircleIcon class="w-3.5 h-3.5" /> {{ formErrors.nombre }}
              </p>
            </div>

            <div>
              <label class="block text-slate-400 text-xs font-bold mb-2 uppercase tracking-wide">Correo Electrónico</label>
              <input 
                v-model="email" 
                type="email" 
                @input="formErrors.email = ''"
                class="w-full bg-slate-50/50 border border-slate-200/80 text-slate-900 px-4 py-3.5 rounded-xl focus:ring-2 focus:ring-blue-500 outline-none transition-all placeholder:text-slate-400 font-medium"
                placeholder="name@company.com"
              />
              <p v-if="formErrors.email" class="text-rose-500 text-xs font-medium mt-1.5 flex items-center gap-1">
                <AlertCircleIcon class="w-3.5 h-3.5" /> {{ formErrors.email }}
              </p>
            </div>

            <div>
              <label class="block text-slate-400 text-xs font-bold mb-2 uppercase tracking-wide">Contraseña</label>
              <input 
                v-model="password" 
                type="password" 
                @input="formErrors.password = ''"
                class="w-full bg-slate-50/50 border border-slate-200/80 text-slate-900 px-4 py-3.5 rounded-xl focus:ring-2 focus:ring-blue-500 outline-none transition-all placeholder:text-slate-400 font-medium"
                placeholder="••••••••"
              />
              <p v-if="formErrors.password" class="text-rose-500 text-xs font-medium mt-1.5 flex items-center gap-1">
                <AlertCircleIcon class="w-3.5 h-3.5" /> {{ formErrors.password }}
              </p>
            </div>

            <button 
              type="submit" 
              :disabled="authStore.loading"
              class="w-full bg-blue-600 hover:bg-blue-500 text-white font-medium py-3.5 rounded-xl transition-all active:scale-[0.98] disabled:opacity-50 flex items-center justify-center"
            >
              <span v-if="!authStore.loading">Registrarse Ahora</span>
              <Loader2Icon v-else class="w-5 h-5 animate-spin" />
            </button>
            <ErrorMessage :message="authStore.error" />
          </form>

          <div class="mt-8 pt-8 border-t border-slate-200 text-center">
            <p class="text-slate-500 font-medium text-sm">
              ¿Ya tenés una cuenta? 
              <router-link to="/" class="text-blue-500 font-semibold hover:text-blue-600 transition-colors ml-1">Iniciar Sesión</router-link>
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
import { TicketIcon, Loader2Icon, AlertCircleIcon } from 'lucide-vue-next';
import ErrorMessage from '../components/ErrorMessage.vue';

const authStore = useAuthStore();
const notificationStore = useNotificationStore();
const router = useRouter();

const nombre = ref('');
const email = ref('');
const password = ref('');
const formErrors = ref({ nombre: '', email: '', password: '' });

const handleRegister = async () => {
  formErrors.value = { nombre: '', email: '', password: '' };
  let hasError = false;

  if (!nombre.value) {
    formErrors.value.nombre = 'Tu nombre completo es requerido';
    hasError = true;
  }
  if (!email.value) {
    formErrors.value.email = 'El correo electrónico es requerido';
    hasError = true;
  }
  if (!password.value) {
    formErrors.value.password = 'La contraseña es requerida';
    hasError = true;
  } else if (password.value.length < 6) {
    formErrors.value.password = 'La contraseña debe tener al menos 6 caracteres';
    hasError = true;
  }

  if (hasError) return;

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
