import { Request, Response } from "express";
import dao from "../dao/dispositivoDAO";
import jwt from 'jsonwebtoken';
import keySecret from "../config/keysSecret";
import validator from 'validator';

class DispositivoController {

    public async listar(req: Request, res: Response) {
        try {

            const result = await dao.estatusDispositivo();

            res.json(result);
        } catch (error: any) {
            return res.status(500).json({ message : `${error.message}` });
        }
    }

    public async actualizarDispositivo(req: Request, res: Response) {
        try {

            // se actualiza el estatus 
            const result = await dao.actualizarDispositivo();

            if (result.affectedRows > 0) {
                return res.json({message: "Los datos se actualizaron correctamente", code: 0});
            } else {
                return res.status(404).json({ message: result.message, code: 1});
            }
            
        } catch (error: any) {
            return res.status(500).json({ message : `${error.message}` });
        }
    }

}
export const dispositivoController = new DispositivoController();