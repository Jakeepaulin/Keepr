import "@mdi/font/css/materialdesignicons.css";
import "bootstrap";
import { createApp } from "vue";
import "animate.css";
// @ts-ignore
import App from "./App.vue";
import { registerGlobalComponents } from "./registerGlobalComponents";
import { router } from "./router";

const root = createApp(App);
registerGlobalComponents(root);

root.use(router).mount("#app");
