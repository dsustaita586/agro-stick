"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = require("express");
class IndexRoutes {
    constructor() {
        this.router = (0, express_1.Router)();
        this.config();
    }
    config() {
        // route GET
        this.router.get('/', (req, res) => {
            res.json({ mensaje: "PETICION PARA LISTAR" });
        });
        this.router.post('/', (req, res) => {
            res.json({ mensaje: "PETICION PARA LISTAR (POST)" });
        });
        this.router.put('/', (req, res) => {
            res.json({ mensaje: "PETICION PARA LISTAR (PUT)" });
        });
        this.router.delete('/', (req, res) => {
            res.json({ mensaje: "PETICION PARA LISTAR (DELETE)" });
        });
        this.router.get('/roles', (req, res) => {
            res.json({ mensaje: "PETICION PARA LISTAR LOS ROLES" });
        });
    }
}
const indexRoutes = new IndexRoutes();
exports.default = indexRoutes.router;
