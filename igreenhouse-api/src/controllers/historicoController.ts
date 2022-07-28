import { Request, Response } from "express";
import dao from "../dao/historicoDAO";
import jwt from 'jsonwebtoken';
import keySecret from "../config/keysSecret";
import validator from 'validator';

class HistoricoController {

    public async listar(req: Request, res: Response) {
        try {

            // obtener los datos del body
            const { inicio, fin, ...rest } = req.body;

            // Se verifica la estructura de la petición
            if (Object.keys(rest).length > 0) {
                return res.status(400).json({ message : "La estructura no es correcta", code: 1 });
            }

            // Verificar que los datos "inicio" y "fin" existan
             if (!inicio || !fin) {
                return res.status(400).json({ message : "Todos los campos son requeridos", code: 1});
            }

            // verificar que los datos no esten vacios
            if (validator.isEmpty(inicio.trim())
                || validator.isEmpty(fin.trim())) {
                    return res.status(400).json({ message : "Todos los campos son requeridos", code: 1 });
            }

            const temps = await dao.getTempByDates(inicio, fin);
            const hums = await dao.getHumByDates(inicio, fin);
            const ppms = await dao.getPPMByDates(inicio, fin);

            const lst = {
                "temperatura" : temps,
                "humedad" : hums,
                "ppm" : ppms
            };

            return res.json(lst);
            
        } catch (error: any) {
            return res.status(500).json({ message : `${error.message}` });
        }
    }

}
export const historicoController = new HistoricoController();