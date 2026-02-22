<template>
  <div class="min-h-screen bg-slate-950 text-slate-200 p-10 font-['Outfit']">
    <div class="max-w-4xl mx-auto">
      <div class="flex items-center gap-4 mb-12">
        <button @click="router.back()" class="p-3 bg-white/5 hover:bg-white/10 text-slate-400 rounded-2xl transition-all">
          <ArrowLeftIcon class="w-6 h-6" />
        </button>
        <h2 class="text-4xl font-black text-white italic tracking-tighter uppercase">Configuración</h2>
      </div>

      <div class="space-y-12">
        <!-- Theme Section -->
        <section class="bg-slate-900/50 border border-white/5 rounded-[3rem] p-10 backdrop-blur-3xl">
          <div class="flex items-center gap-4 mb-8">
            <PaletteIcon class="w-6 h-6 text-blue-500" />
            <h3 class="text-xl font-black text-white uppercase italic">Apariencia</h3>
          </div>

          <div class="grid grid-cols-2 sm:grid-cols-4 gap-6">
            <button 
              v-for="color in ['blue', 'indigo', 'emerald', 'rose']" 
              :key="color"
              @click="settingsStore.setThemeColor(color)"
              class="group relative h-32 rounded-3xl border-2 transition-all overflow-hidden"
              :class="[
                settingsStore.themeColor === color ? 'border-primary scale-[1.05]' : 'border-white/5 hover:border-white/20'
              ]"
            >
              <div :class="`absolute inset-0 bg-${color}-500/20`"></div>
              <div class="absolute inset-0 flex items-center justify-center">
                <div :class="`w-8 h-8 rounded-full bg-${color}-500 shadow-lg`"></div>
              </div>
              <div class="absolute bottom-3 left-0 w-full text-center text-[8px] font-black uppercase tracking-widest text-white/40">
                {{ i18n[settingsStore.language]?.[color] || color }}
              </div>
            </button>
          </div>
        </section>

        <!-- UI Density -->
        <section class="bg-slate-900/50 border border-white/5 rounded-[3rem] p-10 backdrop-blur-3xl">
          <div class="flex items-center gap-4 mb-8">
            <LayersIcon class="w-6 h-6 text-blue-500" />
            <h3 class="text-xl font-black text-white uppercase italic">Densidad de Interfaz</h3>
          </div>

          <div class="flex gap-4">
            <button 
              @click="settingsStore.setUIDensity('comfortable')"
              class="flex-1 py-4 rounded-2xl font-black uppercase tracking-widest text-xs transition-all border-2"
              :class="settingsStore.uiDensity === 'comfortable' ? 'bg-primary border-primary text-white' : 'bg-white/5 border-white/5 text-slate-500'"
            >
              {{ i18n[settingsStore.language]?.comfortable }}
            </button>
            <button 
              @click="settingsStore.setUIDensity('compact')"
              class="flex-1 py-4 rounded-2xl font-black uppercase tracking-widest text-xs transition-all border-2"
              :class="settingsStore.uiDensity === 'compact' ? 'bg-primary border-primary text-white' : 'bg-white/5 border-white/5 text-slate-500'"
            >
              {{ i18n[settingsStore.language]?.compact }}
            </button>
          </div>
        </section>

        <!-- System Settings -->
        <section class="bg-slate-900/50 border border-white/5 rounded-[3rem] p-10 backdrop-blur-3xl">
          <div class="flex items-center gap-4 mb-8">
            <Settings2Icon class="w-6 h-6 text-blue-500" />
            <h3 class="text-xl font-black text-white uppercase italic">Sistema</h3>
          </div>

          <div class="space-y-4">
            <div class="flex items-center justify-between p-6 bg-white/[0.02] rounded-3xl border border-white/5">
              <div>
                <p class="text-sm font-bold text-white">{{ i18n[settingsStore.language]?.notifications }}</p>
                <p class="text-[10px] text-slate-500 font-bold uppercase tracking-widest">{{ i18n[settingsStore.language]?.notifDesc }}</p>
              </div>
              <div 
                @click="settingsStore.toggleNotifications()"
                class="w-12 h-6 rounded-full relative cursor-pointer transition-all border border-white/10"
                :class="settingsStore.notificationsEnabled ? 'bg-primary/30' : 'bg-slate-800'"
              >
                <div 
                  class="absolute top-1 w-4 h-4 bg-white rounded-full transition-all shadow-lg shadow-black/50"
                  :class="settingsStore.notificationsEnabled ? 'left-7' : 'left-1'"
                ></div>
              </div>
            </div>
            
            <div class="p-6 bg-white/[0.02] rounded-3xl border border-white/5">
              <div class="flex items-center justify-between mb-4">
                <div>
                  <p class="text-sm font-bold text-white">{{ i18n[settingsStore.language]?.language }}</p>
                  <p class="text-[10px] text-slate-500 font-bold uppercase tracking-widest">{{ i18n[settingsStore.language]?.langName }}</p>
                </div>
              </div>
              <div class="flex gap-2">
                <button 
                  v-for="(label, code) in {es: 'ES', en: 'EN', fr: 'FR'}" 
                  :key="code"
                  @click="settingsStore.setLanguage(code)"
                  class="flex-1 py-2 rounded-xl font-black text-[10px] transition-all border"
                  :class="settingsStore.language === code ? 'bg-primary/20 border-primary text-white' : 'bg-white/5 border-white/5 text-slate-500'"
                >
                  {{ label }}
                </button>
              </div>
            </div>
          </div>
        </section>
      </div>
    </div>
  </div>
</template>

<script setup>
import { useRouter } from 'vue-router';
import { 
  ArrowLeftIcon, PaletteIcon, LayersIcon, Settings2Icon, 
  ChevronRightIcon 
} from 'lucide-vue-next';
import { useSettingsStore } from '../store/settings';

const router = useRouter();
const settingsStore = useSettingsStore();

const i18n = {
  es: {
    title: 'Configuración', appearance: 'Apariencia', density: 'Densidad', system: 'Sistema',
    comfortable: 'Cómoda', compact: 'Compacta', notifications: 'Notificaciones', 
    notifDesc: 'Alertas en tiempo real', language: 'Idioma', langName: 'Español',
    blue: 'Azul', indigo: 'Índigo', emerald: 'Esmeralda', rose: 'Rosa'
  },
  en: {
    title: 'Settings', appearance: 'Appearance', density: 'Density', system: 'System',
    comfortable: 'Comfortable', compact: 'Compact', notifications: 'Notifications',
    notifDesc: 'Real-time alerts', language: 'Language', langName: 'English',
    blue: 'Blue', indigo: 'Indigo', emerald: 'Emerald', rose: 'Rose'
  },
  fr: {
    title: 'Paramètres', appearance: 'Apparence', density: 'Densité', system: 'Système',
    comfortable: 'Confortable', compact: 'Compact', notifications: 'Notifications',
    notifDesc: 'Alertes en temps réel', language: 'Langue', langName: 'Français',
    blue: 'Bleu', indigo: 'Indigo', emerald: 'Émeraude', rose: 'Rose'
  }
};
</script>

<style scoped>
.bg-primary { background-color: rgb(var(--accent-primary)); }
.border-primary { border-color: rgb(var(--accent-primary)); }
.text-primary { color: rgb(var(--accent-primary)); }
</style>
