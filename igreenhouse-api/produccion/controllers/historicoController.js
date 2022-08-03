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
var __rest = (this && this.__rest) || function (s, e) {
    var t = {};
    for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p) && e.indexOf(p) < 0)
        t[p] = s[p];
    if (s != null && typeof Object.getOwnPropertySymbols === "function")
        for (var i = 0, p = Object.getOwnPropertySymbols(s); i < p.length; i++) {
            if (e.indexOf(p[i]) < 0 && Object.prototype.propertyIsEnumerable.call(s, p[i]))
                t[p[i]] = s[p[i]];
        }
    return t;
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.historicoController = void 0;
const historicoDAO_1 = __importDefault(require("../dao/historicoDAO"));
const validator_1 = __importDefault(require("validator"));
class HistoricoController {
    listar(req, res) {
        return __awaiter(this, void 0, void 0, function* () {
            try {
                // obtener los datos del body
                const _a = req.body, { inicio, fin } = _a, rest = __rest(_a, ["inicio", "fin"]);
                // Se verifica la estructura de la petición
                if (Object.keys(rest).length > 0) {
                    return res.status(400).json({ message: "La estructura no es correcta", code: 1 });
                }
                // Verificar que los datos "inicio" y "fin" existan
                if (!inicio || !fin) {
                    return res.status(400).json({ message: "Todos los campos son requeridos", code: 1 });
                }
                // verificar que los datos no esten vacios
                if (validator_1.default.isEmpty(inicio.trim())
                    || validator_1.default.isEmpty(fin.trim())) {
                    return res.status(400).json({ message: "Todos los campos son requeridos", code: 1 });
                }
                const temps = yield historicoDAO_1.default.getTempByDates(inicio, fin);
                const hums = yield historicoDAO_1.default.getHumByDates(inicio, fin);
                const ppms = yield historicoDAO_1.default.getPPMByDates(inicio, fin);
                const lst = {
                    "temperatura": temps,
                    "humedad": hums,
                    "ppm": ppms
                };
                return res.json(lst);
            }
            catch (error) {
                return res.status(500).json({ message: `${error.message}` });
            }
        });
    }
}
exports.historicoController = new HistoricoController();
