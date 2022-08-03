import { Router } from "express";
import { dispositivoController } from "../controllers/dispositivoController";

class DispositivoRoutes {

    public router: Router;

    constructor() {
        this.router = Router();
        this.config();
    }

    config() {
        this.router.get('/', dispositivoController.listar);
        this.router.post('/', dispositivoController.actualizarDispositivo);
    }

}
const dispositivoRoutes = new DispositivoRoutes();
export default dispositivoRoutes.router;