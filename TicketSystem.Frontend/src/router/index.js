import { createRouter, createWebHistory } from 'vue-router';
import { useAuthStore } from '../store/auth';

const routes = [
    {
        path: '/',
        component: () => import('../views/LoginView.vue'),
        meta: { public: true }
    },
    {
        path: '/register',
        component: () => import('../views/RegisterView.vue'),
        meta: { public: true }
    },
    {
        path: '/dashboard',
        alias: ['/tickets'],
        component: () => import('../views/UserDashboard.vue'),
        meta: { requiresAuth: true }
    },
    {
        path: '/admin',
        alias: ['/admin/tickets', '/admin/users'],
        component: () => import('../views/AdminDashboard.vue'),
        meta: { requiresAuth: true, requiresAdmin: true }
    },
    {
        path: '/settings',
        component: () => import('../views/SettingsView.vue'),
        meta: { requiresAuth: true }
    },
    {
        path: '/profile',
        component: () => import('../views/ProfileView.vue'),
        meta: { requiresAuth: true }
    },
    {
        path: '/:pathMatch(.*)*',
        redirect: '/'
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

router.beforeEach((to, from, next) => {
    const authStore = useAuthStore();

    if (to.meta.requiresAuth && !authStore.isAuthenticated) {
        next('/');
    } else if (to.meta.requiresAdmin && !authStore.isAdmin && !authStore.isOperador) {
        next('/dashboard');
    } else {
        next();
    }
});

export default router;
