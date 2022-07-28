import pool from '../database/database';
class HistoricoDAO {

    public async getTempByDates(inicio: string, fin: string) {
        const result = await pool.then(async (connection) => {
            return await connection.query(
                " SELECT DATE(fechaRegistro) as fecha, SUM(temperatura) / COUNT(cveTemperatura) as dato "
                + " FROM tbl_temperatura "
                + " WHERE DATE(fechaRegistro) BETWEEN ? AND ? "
                + " GROUP BY DATE(fechaRegistro) ", [inicio, fin]);
        });
        return result;
    }

    public async getHumByDates(inicio: string, fin: string) {
        const result = await pool.then(async (connection) => {
            return await connection.query(
                " SELECT DATE(fechaRegistro) as fecha, SUM(humedad) / COUNT(cveHumedad) as dato "
                + " FROM tbl_humedad "
                + " WHERE DATE(fechaRegistro) BETWEEN ? AND ? "
                + " GROUP BY DATE(fechaRegistro) ", [inicio, fin]);
        });
        return result;
    }

    public async getPPMByDates(inicio: string, fin: string) {
        const result = await pool.then(async (connection) => {
            return await connection.query(
                " SELECT DATE(fechaRegistro) as fecha, SUM(ppm) / COUNT(cvePpm) as dato "
                + " FROM tbl_ppm "
                + " WHERE DATE(fechaRegistro) BETWEEN ? AND ? "
                + " GROUP BY DATE(fechaRegistro) ", [inicio, fin]);
        });
        return result;
    }
}
const dao = new HistoricoDAO();
export default dao;