import { defineStore } from 'pinia';

export const useSettingsStore = defineStore('settings', {
    state: () => ({
        themeColor: localStorage.getItem('themeColor') || 'blue',
        uiDensity: localStorage.getItem('uiDensity') || 'comfortable',
        language: localStorage.getItem('language') || 'es',
        notificationsEnabled: localStorage.getItem('notificationsEnabled') !== 'false'
    }),
    actions: {
        setThemeColor(color) {
            this.themeColor = color;
            localStorage.setItem('themeColor', color);
            this.applyTheme();
        },
        setUIDensity(density) {
            this.uiDensity = density;
            localStorage.setItem('uiDensity', density);
            this.applyDensity();
        },
        setLanguage(lang) {
            this.language = lang;
            localStorage.setItem('language', lang);
        },
        toggleNotifications() {
            this.notificationsEnabled = !this.notificationsEnabled;
            localStorage.setItem('notificationsEnabled', this.notificationsEnabled);
        },
        applyTheme() {
            document.documentElement.className = `theme-${this.themeColor}`;
        },
        applyDensity() {
            document.documentElement.setAttribute('data-density', this.uiDensity);
        },
        initSettings() {
            this.applyTheme();
            this.applyDensity();
        }
    },
    getters: {
        themeClasses: (state) => {
            const colors = {
                blue: 'from-blue-600 to-indigo-600 shadow-blue-500/20',
                indigo: 'from-indigo-600 to-violet-600 shadow-indigo-500/20',
                emerald: 'from-emerald-600 to-teal-600 shadow-emerald-500/20',
                rose: 'from-rose-600 to-pink-600 shadow-rose-500/20'
            };
            return colors[state.themeColor] || colors.blue;
        },
        accentColor: (state) => {
            const colors = {
                blue: 'text-blue-400',
                indigo: 'text-indigo-400',
                emerald: 'text-emerald-400',
                rose: 'text-rose-400'
            };
            return colors[state.themeColor] || colors.blue;
        }
    }
});
