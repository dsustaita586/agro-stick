import express, { Application } from 'express';
import indexRoutes from './routes/indexRoutes';

class Server {
    public app: Application;

    constructor() {
        this.app = express();

        this.config();
        this.routes();
    }

    config() {
        this.app.set("port", 3000);
    }

    routes() { 
        this.app.use('/', indexRoutes);
    }

    start() {
        this.app.listen(this.app.get("port"), () => {
            console.log("Server on port", this.app.get("port"));
        });
    }
}

const server = new Server();
server.start();