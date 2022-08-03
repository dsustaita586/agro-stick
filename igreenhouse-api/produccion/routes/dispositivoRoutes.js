"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = require("express");
const dispositivoController_1 = require("../controllers/dispositivoController");
class DispositivoRoutes {
    constructor() {
        this.router = (0, express_1.Router)();
        this.config();
    }
    config() {
        this.router.get('/', dispositivoController_1.dispositivoController.listar);
        this.router.post('/', dispositivoController_1.dispositivoController.actualizarDispositivo);
    }
}
const dispositivoRoutes = new DispositivoRoutes();
exports.default = dispositivoRoutes.router;
