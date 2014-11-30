package reader;

import java.sql.*;

class DBTest {

    public static void main(String[] args) {
        
        try {
            Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
        } catch ( Exception e ) {
            System.out.println("error");
        }
        
        String dbURL = "jdbc:mysql://localhost:3306/CivionicsContext";
        
        Statement stmt = null;
        ResultSet resSet = null;
        
        Connection conn = null;
        
        try {
            conn = DriverManager.getConnection(dbURL);
            stmt = conn.createStatement();
            resSet = stmt.executeQuery("SELECT VERSION()");
        } catch (SQLException se) {
            System.out.println("SQLException caught");
            se.printStackTrace();
        }
    }
}