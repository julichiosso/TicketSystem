import { createApp } from 'vue';
import { createPinia } from 'pinia';
import router from './router';
import App from './App.vue';
import './style.css';

const app = createApp(App);
const pinia = createPinia();
app.use(pinia);
app.use(router);

// initialize settings (theme, density, etc) right away
import { useSettingsStore } from './store/settings';
const settings = useSettingsStore(pinia);
settings.initSettings();

app.mount('#app');
