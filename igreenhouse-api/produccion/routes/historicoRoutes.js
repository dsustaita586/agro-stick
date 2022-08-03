"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = require("express");
const historicoController_1 = require("../controllers/historicoController");
class HistoricoRoutes {
    constructor() {
        this.router = (0, express_1.Router)();
        this.config();
    }
    config() {
        this.router.post('/', historicoController_1.historicoController.listar);
    }
}
const historicoRoutes = new HistoricoRoutes();
exports.default = historicoRoutes.router;
