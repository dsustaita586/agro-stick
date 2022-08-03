import pool from '../database/database';
class DispositivoDAO {

    public async actualizarDispositivo() {
        const result = await pool.then(async (connection) => {
            return await connection.query(
                " UPDATE tbl_dispositivo SET estatus = !estatus ");
        });
        return result;
    }

    public async estatusDispositivo() {
        const result = await pool.then(async (connection) => {
            return await connection.query(
                " SELECT estatus FROM tbl_dispositivo WHERE cveDispositivo = ? ", [1]);
        });
        return result;
    }
}
const dao = new DispositivoDAO();
export default dao;