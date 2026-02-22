<template>
  <span :class="badgeClass" class="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-semibold tracking-wide">
    {{ displayLabel }}
  </span>
</template>

<script setup>
import { computed } from 'vue';

const props = defineProps({
  type: String,
  label: String
});

const badgeClass = computed(() => {
  const typeStr = String(props.type || '').toLowerCase();
  const types = {
    // Priorities
    'high': 'bg-rose-500/10 text-rose-500 border border-rose-500/20',
    'medium': 'bg-amber-500/10 text-amber-500 border border-amber-500/20',
    'low': 'bg-emerald-500/10 text-emerald-500 border border-emerald-500/20',
    // Status
    'open': 'bg-blue-500/10 text-blue-500 border border-blue-500/20',
    'inprogress': 'bg-indigo-500/10 text-indigo-500 border border-indigo-500/20',
    'in progress': 'bg-indigo-500/10 text-indigo-500 border border-indigo-500/20',
    'resolved': 'bg-emerald-500/10 text-emerald-500 border border-emerald-500/20',
    'closed': 'bg-slate-500/10 text-slate-500 border border-slate-500/20',
    // Spanish enums
    'alta': 'bg-rose-500/10 text-rose-500 border border-rose-500/20',
    'media': 'bg-amber-500/10 text-amber-500 border border-amber-500/20',
    'baja': 'bg-emerald-500/10 text-emerald-500 border border-emerald-500/20',
    'pendiente': 'bg-blue-500/10 text-blue-500 border border-blue-500/20',
    'enproceso': 'bg-indigo-500/10 text-indigo-500 border border-indigo-500/20',
    'resuelto': 'bg-emerald-500/10 text-emerald-500 border border-emerald-500/20',
    // Numeric
    '0': 'bg-blue-500/10 text-blue-500 border border-blue-500/20',
    '1': 'bg-indigo-500/10 text-indigo-500 border border-indigo-500/20',
    '2': 'bg-emerald-500/10 text-emerald-500 border border-emerald-500/20',
    '3': 'bg-slate-500/10 text-slate-500 border border-slate-500/20',
    'primary': 'bg-primary/10 text-primary border border-primary/20'
  };
  return types[typeStr] || 'bg-slate-500/10 text-slate-500 border border-slate-500/20';
});

const displayLabel = computed(() => {
  const labelStr = String(props.label || '').toLowerCase().replace(' ', '');
  const translations = {
    'open': 'Abierto',
    'inprogress': 'En Progreso',
    'resolved': 'Resuelto',
    'closed': 'Cerrado',
    'high': 'Alta',
    'medium': 'Media',
    'low': 'Baja',
    'pendiente': 'Abierto',
    'enproceso': 'En Progreso',
    'resuelto': 'Resuelto',
    'alta': 'Alta',
    'media': 'Media',
    'baja': 'Baja',
    '0': 'Abierto',
    '1': 'En Progreso',
    '2': 'Resuelto',
    '3': 'Cerrado'
  };
  return translations[labelStr] || props.label;
});
</script>
