import pool from '../database/database';
class AuthDAO {

    public async getuserByusername(username: string) {
        const result = await pool.then(async (connection) => {
            return await connection.query(
                " SELECT * FROM tbl_usuario WHERE username = ? ", [username]);
        });
        return result;
    }
}
const dao = new AuthDAO();
export default dao;