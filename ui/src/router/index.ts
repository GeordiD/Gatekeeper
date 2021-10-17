import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import LoginView from "../views/LoginView.vue";
import HomeView from "../views/HomeView.vue";
import { _authStore } from "@/store/AuthStore";

const routes: Array<RouteRecordRaw> = [
    {
        path: "/",
        name: "home",
        component: HomeView,
        beforeEnter: (to, from, next) => {
            if (!_authStore.getState().tokenResponse) {
                next({ path: "/login" });
            } else {
                next();
            }
        },
    },
    {
        path: "/login",
        name: "Login",
        component: LoginView,
    },
    {
        path: "/orders",
        name: "Orders",
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import("../views/OrdersView.vue"),
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;
