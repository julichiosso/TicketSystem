<template>
 <span :class="badgeClass" class="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-semibold tracking-wide">
 {{ displayLabel }}
 </span>
</template>

<script setup>
import { computed } from 'vue';

const props = defineProps({
 type: [String, Number],
 label: [String, Number]
});

const badgeClass = computed(() => {
 const typeStr = String(props.type || '').toLowerCase().replaceAll(' ', '');
 const types = {
 alta: 'bg-rose-500/10 text-rose-500 border border-rose-500/20',
 media: 'bg-amber-500/10 text-amber-500 border border-amber-500/20',
 baja: 'bg-emerald-500/10 text-emerald-500 border border-emerald-500/20',
 pendiente: 'bg-blue-500/10 text-blue-500 border border-blue-500/20',
 enproceso: 'bg-indigo-500/10 text-indigo-500 border border-indigo-500/20',
 resuelto: 'bg-emerald-500/10 text-emerald-500 border border-emerald-500/20',
 cerrado: 'bg-slate-500/10 text-slate-500 border border-slate-500/20',
 esperandousuario: 'bg-amber-500/10 text-amber-400 border border-amber-500/20 sla-pulse',
 waitinguser: 'bg-amber-500/10 text-amber-400 border border-amber-500/20 sla-pulse',
 high: 'bg-rose-500/10 text-rose-500 border border-rose-500/20',
 medium: 'bg-amber-500/10 text-amber-500 border border-amber-500/20',
 low: 'bg-emerald-500/10 text-emerald-500 border border-emerald-500/20',
 open: 'bg-blue-500/10 text-blue-500 border border-blue-500/20',
 inprogress: 'bg-indigo-500/10 text-indigo-500 border border-indigo-500/20',
 resolved: 'bg-emerald-500/10 text-emerald-500 border border-emerald-500/20',
 closed: 'bg-slate-500/10 text-slate-500 border border-slate-500/20',
 0: 'bg-blue-500/10 text-blue-500 border border-blue-500/20',
 1: 'bg-indigo-500/10 text-indigo-500 border border-indigo-500/20',
 2: 'bg-emerald-500/10 text-emerald-500 border border-emerald-500/20',
 3: 'bg-slate-500/10 text-slate-500 border border-slate-500/20',
 4: 'bg-amber-500/10 text-amber-400 border border-amber-500/20 sla-pulse'
 };

 return types[typeStr] || 'bg-slate-500/10 text-slate-500 border border-slate-500/20';
});

const displayLabel = computed(() => {
 const labelStr = String(props.label ?? props.type ?? '').toLowerCase().replaceAll(' ', '');
 const translations = {
 open: 'Abierto',
 inprogress: 'En Progreso',
 resolved: 'Resuelto',
 closed: 'Cerrado',
 waitinguser: 'En espera de usuario',
 pendiente: 'Abierto',
 enproceso: 'En Progreso',
 resuelto: 'Resuelto',
 cerrado: 'Cerrado',
 esperandousuario: 'En espera de usuario',
 alta: 'Alta',
 media: 'Media',
 baja: 'Baja',
 high: 'Alta',
 medium: 'Media',
 low: 'Baja',
 0: 'Abierto',
 1: 'En Progreso',
 2: 'Resuelto',
 3: 'Cerrado',
 4: 'En espera de usuario'
 };

 return translations[labelStr] || props.label || props.type;
});
</script>
