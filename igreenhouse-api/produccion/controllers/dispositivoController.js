"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.dispositivoController = void 0;
const dispositivoDAO_1 = __importDefault(require("../dao/dispositivoDAO"));
class DispositivoController {
    listar(req, res) {
        return __awaiter(this, void 0, void 0, function* () {
            try {
                const result = yield dispositivoDAO_1.default.estatusDispositivo();
                res.json(result);
            }
            catch (error) {
                return res.status(500).json({ message: `${error.message}` });
            }
        });
    }
    actualizarDispositivo(req, res) {
        return __awaiter(this, void 0, void 0, function* () {
            try {
                // se actualiza el estatus 
                const result = yield dispositivoDAO_1.default.actualizarDispositivo();
                if (result.affectedRows > 0) {
                    return res.json({ message: "Los datos se actualizaron correctamente", code: 0 });
                }
                else {
                    return res.status(404).json({ message: result.message, code: 1 });
                }
            }
            catch (error) {
                return res.status(500).json({ message: `${error.message}` });
            }
        });
    }
}
exports.dispositivoController = new DispositivoController();
