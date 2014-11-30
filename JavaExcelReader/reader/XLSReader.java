package reader;

import java.sql.*;

//import java.util.MichaelChimicksFace;
import java.util.List;
import java.nio.file.Files;
import java.io.File;
import java.io.FileOutputStream;
import java.io.PrintStream;
import java.io.BufferedInputStream;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.Iterator;

import org.openxmlformats.schemas.spreadsheetml.x2006.main.CTWorksheet;
import org.openxmlformats.schemas.spreadsheetml.x2006.main.CTSheetData;
import org.openxmlformats.schemas.spreadsheetml.x2006.main.CTRow;

import org.apache.poi.poifs.filesystem.POIFSFileSystem;

import org.apache.poi.ss.usermodel.Cell;
import org.apache.poi.ss.usermodel.Row;
import org.apache.poi.xssf.usermodel.XSSFCell;
import org.apache.poi.xssf.usermodel.XSSFRow;
import org.apache.poi.xssf.usermodel.XSSFWorkbook;
import org.apache.poi.xssf.usermodel.XSSFSheet;

class XLSReader {
    
    public enum sensorType{
        ACCELERATION, CORROSION 
    }
    
    static String path;
    static String serialNo;
    static int minVal;
    static int maxVal;
    static int sensorID;
    
    static Connection conn;
    
    static XSSFRow row;
    static XSSFCell cell;
    
    static XSSFWorkbook wb;
    static XSSFSheet sheet;
    
    
    // required arguments: file path, sensor serial No, min value, max value
    public static void main(String[] args) {
        
        String dbURL = "jdbc:mysql://localhost:3306/CivionicsContext.mdl"; // Database URL
        
        String username = "admin"; // Database credentials
        String password = "rishigupta";
        
        Statement stmt = null;   // A statement to execute
        ResultSet resSet = null; // The result of a query
        
        Connection conn = null;  // The connection to the DB
        
        try {
            conn = DriverManager.getConnection(dbURL); //, username, password);
            stmt = conn.createStatement();
            resSet = stmt.executeQuery("SELECT VERSION()");
        } catch (SQLException se) {
            System.out.println("SQLException caught");
            se.getMessage();
        }        
        
        
        try {
            path = args[0];
            serialNo = args[1];
            minVal = Integer.parseInt(args[2]);
            maxVal = Integer.parseInt(args[3]);
        } catch ( ArrayIndexOutOfBoundsException e ) {
            System.err.println("ERROR: Too Few Arguments. \n"
                               + "Required rrguments are: File Path, "
                               + "Sensor Serial No, Maximum Acceptable Value, "
                               + "and Minimum Acceptable Value" );
            return;
        }
        
        FileInputStream input;
        wb = null;
        
        try {
            input = new FileInputStream(new File(path));
            wb = new XSSFWorkbook(input); // Get the Workbook
            sheet = wb.getSheetAt(0);  // Get the first sheet
            
        } catch ( IOException ioe ) {
            System.err.println(ioe.getMessage());
            ioe.printStackTrace();
            return;
        }
        
        row = sheet.getRow(0);
        
        sensorType sType = sensorType.CORROSION;
        try {
            row.getCell(0);
        } catch ( NullPointerException npe ) {
            sType = sensorType.ACCELERATION;
        }
        
        System.out.println(sType);
        
        sensorID = 0;
        
        try {
            resSet = stmt.executeQuery("SELECT ID FROM Sensor WHERE serial="
                                 + serialNo + " AND TypeId=" + 1 );
            if(resSet.next()) {
                sensorID = resSet.getInt("ID");
            } else {
                // there is no sensor in the DB with that ID
            }
        } catch (SQLException se) {
            System.out.println("SQLException caught");
        } catch (Exception e) {
            System.out.println("Other Exception caught");
            //e.printStackTrace();
        }
        
        if (sType == sensorType.ACCELERATION) {
            addAcceleration(sensorID, minVal, maxVal);
        } else if (sType == sensorType.CORROSION) {
            addCorrosion(sensorID, minVal, maxVal);
        }
        
        return;
    }
    
    static void addAcceleration( int sensorID, int min, int max ) {
        
        Statement stmt = null;
        
        try{
            stmt = conn.createStatement();
        } catch (SQLException se) {
            System.out.println("SQLException caught");
            return;
        } catch (NullPointerException npe) {
            System.out.println("No Conn");
        }
        
        sheet = wb.getSheetAt(1);
        
        CTWorksheet ctWorksheet = ((XSSFSheet)sheet).getCTWorksheet();
        CTSheetData ctSheetData = ctWorksheet.getSheetData();
        List<CTRow> ctRowList = ctSheetData.getRowList();
        
        Row solRow = null;
        Cell dateCell = null;
        Cell valueCell = null;
        
        String query;
        
        for (CTRow ctRow : ctRowList) {
            solRow = new MyRow(ctRow, (XSSFSheet)sheet);
            
            query = "INSERT INTO Reading ( SensorID, LoggedTime, "
                    + "isAnomalous, Data ) VALUES ("
                    + sensorID + ", ";
            
            dateCell = solRow.getCell(1);
            if ( dateCell != null 
                 && dateCell.toString() != "" ) {
                
                query += dateCell.getNumericCellValue();
            }
            
            valueCell = solRow.getCell(2);
            if ( valueCell != null 
                 && valueCell.toString() != "" )
            {
                
                if( valueCell.getNumericCellValue() < min
                        || valueCell.getNumericCellValue() > max )
                {
                    query += "TRUE, ";
                } else {
                    query += "FALSE, ";
                }
                
                query += valueCell.getNumericCellValue();
                
            }
            query += ");";
            
            try {
                stmt.executeQuery( query );
            } catch (SQLException se) {
                System.out.println("SQLException caught");
            } catch (NullPointerException npe) {
                System.out.println("No Connection");
            }
        }
    }
    
    static void addCorrosion( int sensorID, int min, int max ) {
        
        Statement stmt = null;
        
        String query;
        
        try{
            stmt = conn.createStatement();
        } catch (SQLException se) {
            System.out.println("SQLException caught");
            return;
        }
        
        sheet = wb.getSheetAt(1);
        
        CTWorksheet ctWorksheet = ((XSSFSheet)sheet).getCTWorksheet();
        CTSheetData ctSheetData = ctWorksheet.getSheetData();
        List<CTRow> ctRowList = ctSheetData.getRowList();
        Iterator<CTRow> rowIter = ctRowList.listIterator(2);
        
        Row solRow = null;
        
        Cell dateCell = null;
        Cell valueCell = null;
        
        while( rowIter.hasNext() ) {
            CTRow ctRow = rowIter.next();
            solRow = new MyRow(ctRow, (XSSFSheet)sheet);
            
            query = "INSERT INTO Reading ( SensorID, LoggedTime, "
                    + "isAnomalous, Data ) VALUES ("
                    + sensorID + ", ";
            
            dateCell = solRow.getCell(1);
            if ( dateCell != null 
                 && dateCell.toString() != "" ) {
                
                query += dateCell.getNumericCellValue();
            }
            
            valueCell = solRow.getCell(3);
            if ( valueCell != null 
                 && valueCell.toString() != "" )
            {
                
                if( valueCell.getNumericCellValue() < min
                        || valueCell.getNumericCellValue() > max )
                {
                    query += "TRUE, ";
                } else {
                    query += "FALSE, ";
                }
                
                query += valueCell.getNumericCellValue();
                
            }
            query += ");";
        
            try {
                stmt.executeQuery( query );
            } catch (SQLException se) {
                System.out.println("SQLException caught");
            }
        }
        
        
    }
}

class MyRow extends XSSFRow {
    MyRow(org.openxmlformats.schemas.spreadsheetml.x2006.main.CTRow row, XSSFSheet sheet) {
        super(row, sheet);
    }
}
