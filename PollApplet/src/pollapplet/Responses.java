/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package pollapplet;

import com.turningtech.poll.Response;
import java.sql.*;

import java.util.List;

/**
 *
 * @author s4200943
 */
public class Responses {
    
    Responses(){     
    }
    
    public void saveResponses(List<Response> responses, int answerID){
        
        Connection con;
        try {
            // pull all polls for poll master into applet
            DriverManager.registerDriver(new oracle.jdbc.OracleDriver());
            con = DriverManager.getConnection("jdbc:oracle:thin:@oracle.students.itee.uq.edu.au:1521:iteeo", "csse3004gg", "groupg");

            Statement stmt = con.createStatement();
            ResultSet rset = stmt.executeQuery("SELECT MAX(RESPONSE_ID) FROM RESPONSES;");
            rset.next();
            int maxID = Integer.parseInt(rset.getString("MAX(RESPONSE_ID"));
            //String query = ""
            //select max(RESPONSE_ID)
            //FROM Responses;
                    
                    
            //INSERT INTO RESPONSES(response_id, feedback, answer_id, created_at, user_id, session_id)
            for(Response res: responses){
              //  query.concat("("+(maxID++)+", "+res.getResponse()+", "+answerID+", sysdate, ");
                        
                
            }
            
            
            stmt.executeUpdate("insert code here");
            
            stmt.close();
            System.out.println("Ok.");
        //responses.get(0).
        }catch (Exception e){
            
        }
    }
    
}
