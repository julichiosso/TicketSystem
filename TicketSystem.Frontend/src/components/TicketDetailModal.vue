<template>
  <div v-if="ticket" class="fixed inset-0 z-[110] flex items-center justify-center p-6 bg-slate-950/80 backdrop-blur-2xl animate-in fade-in duration-300">
    <div class="bg-slate-900 border border-white/10 rounded-[3rem] w-full max-w-2xl shadow-2xl relative overflow-hidden">
      <!-- Decorative Background -->
      <div class="absolute top-0 right-0 w-64 h-64 bg-blue-500/5 blur-[100px] pointer-events-none"></div>

      <div class="p-10">
        <!-- Header -->
        <div class="flex justify-between items-start mb-10 border-b border-white/5 pb-8">
          <div>
            <div class="flex items-center gap-3 mb-2">
              <span class="text-xs font-mono text-slate-500 bg-slate-950 px-3 py-1 rounded-full border border-slate-800">#{{ ticket.id?.substring(0, 12) }}</span>
              <StatusBadge :type="ticket.estado" :label="ticket.estado" />
              <StatusBadge :type="ticket.prioridad" :label="ticket.prioridad" />
            </div>
            <h3 class="text-3xl font-black text-white italic tracking-tighter uppercase">{{ ticket.titulo }}</h3>
          </div>
          <button @click="$emit('close')" class="p-3 bg-white/5 hover:bg-white/10 text-slate-400 hover:text-white rounded-2xl transition-all active:scale-90">
            <XIcon class="w-6 h-6" />
          </button>
        </div>

        <!-- Content -->
        <div class="space-y-8 max-h-[60vh] overflow-y-auto pr-4 custom-scrollbar">
          <div>
            <h4 class="text-[10px] font-black text-slate-500 uppercase tracking-widest mb-3">Descripcion Completa</h4>
            <p class="text-slate-300 text-lg leading-relaxed font-medium">
              {{ ticket.descripcion }}
            </p>
          </div>

          <div class="grid grid-cols-2 gap-6 pt-4">
            <div class="p-6 bg-white/[0.02] border border-white/5 rounded-3xl">
              <h5 class="text-[10px] font-black text-slate-500 uppercase tracking-widest mb-3">Creado por</h5>
              <div class="flex items-center gap-3">
                <div class="w-9 h-9 rounded-full bg-white/10 flex items-center justify-center text-white text-sm font-black border border-white/20">
                  {{ (ticket.usuarioNombre || '?')[0].toUpperCase() }}
                </div>
                <div class="text-white font-bold">{{ ticket.usuarioNombre }}</div>
              </div>
            </div>
            <div class="p-6 bg-white/[0.02] border border-white/5 rounded-3xl">
              <h5 class="text-[10px] font-black text-slate-500 uppercase tracking-widest mb-3">Fecha de Registro</h5>
              <div class="text-white font-bold">{{ new Date(ticket.fechaCreacion).toLocaleString('es-ES') }}</div>
            </div>
          </div>

          <div>
            <h4 class="text-[10px] font-black text-slate-500 uppercase tracking-widest mb-4">Informacion sobre Estados</h4>
            <div class="space-y-3">
              <div class="flex items-center gap-4 p-4 rounded-2xl" :class="ticket.estado === 'Pendiente' || ticket.estado === 0 ? 'bg-blue-500/10 border border-blue-500/20' : 'opacity-40'">
                <InboxIcon class="w-5 h-5 text-blue-400" />
                <div class="text-sm">
                  <span class="text-white font-bold block">Abierto</span>
                  <span class="text-slate-400 text-xs">Tu ticket fue recibido y esta en espera de ser asignado.</span>
                </div>
              </div>
              <div class="flex items-center gap-4 p-4 rounded-2xl" :class="ticket.estado === 'EnProceso' || ticket.estado === 1 ? 'bg-indigo-500/10 border border-indigo-500/20' : 'opacity-40'">
                <TimerIcon class="w-5 h-5 text-indigo-400" />
                <div class="text-sm">
                  <span class="text-white font-bold block">En Progreso</span>
                  <span class="text-slate-400 text-xs">Un operador esta trabajando activamente en tu solicitud.</span>
                </div>
              </div>
              <div class="flex items-center gap-4 p-4 rounded-2xl" :class="ticket.estado === 'Resuelto' || ticket.estado === 2 ? 'bg-emerald-500/10 border border-emerald-500/20' : 'opacity-40'">
                <CheckCircleIcon class="w-5 h-5 text-emerald-400" />
                <div class="text-sm">
                  <span class="text-white font-bold block">Resuelto</span>
                  <span class="text-slate-400 text-xs">Se encontro una solucion y el ticket esta completado.</span>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div class="pt-8 mt-4 border-t border-white/5">
          <button @click="$emit('close')" class="w-full py-4 bg-slate-800 hover:bg-white/10 text-white font-black text-xs uppercase tracking-widest rounded-2xl transition-all">Cerrar Detalle</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { XIcon, InboxIcon, TimerIcon, CheckCircleIcon } from 'lucide-vue-next';
import StatusBadge from './StatusBadge.vue';

defineProps({
  ticket: Object
});

defineEmits(['close']);
</script>
