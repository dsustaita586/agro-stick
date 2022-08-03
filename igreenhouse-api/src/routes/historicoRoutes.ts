import { Router } from "express";
import { historicoController } from "../controllers/historicoController";

class HistoricoRoutes {

    public router: Router;

    constructor() {
        this.router = Router();
        this.config();
    }

    config() {
        this.router.post('/', historicoController.listar);
    }

}
const historicoRoutes = new HistoricoRoutes();
export default historicoRoutes.router;