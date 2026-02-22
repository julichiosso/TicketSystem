<template>
  <router-view />
  <NotificationToast />
</template>

<script setup>
import { onMounted } from 'vue';
import NotificationToast from './components/NotificationToast.vue';
import { useSettingsStore } from './store/settings';

const settingsStore = useSettingsStore();

onMounted(() => {
  settingsStore.initSettings();
});
</script>
