import { Router } from "express";

class IndexRoutes {
    public router: Router = Router();

    constructor() {
        this.config();
    }

    config(): void {
        // route GET
        this.router.get('/', (req, res) => {
            
            res.json({mensaje : "PETICION PARA LISTAR"});
        });

        this.router.post('/', (req, res) => {
            res.json({mensaje : "PETICION PARA LISTAR (POST)"});
        });

        this.router.put('/', (req, res) => {
            res.json({mensaje : "PETICION PARA LISTAR (PUT)"});
        });

        this.router.delete('/', (req, res) => {
            res.json({mensaje : "PETICION PARA LISTAR (DELETE)"});
        });

        this.router.get('/roles', (req, res) => {
            res.json({mensaje : "PETICION PARA LISTAR LOS ROLES"});
        });
    }
}
const indexRoutes = new IndexRoutes();
export default indexRoutes.router;