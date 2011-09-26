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

    Responses() {
    }

    public void saveResponses(List<Response> responses, int sessionID, int answerID) {

        Connection con;
        String query = "INSERT ALL\n";
        try {
            DriverManager.registerDriver(new oracle.jdbc.OracleDriver());
            con = DriverManager.getConnection("jdbc:oracle:thin:@oracle.students.itee.uq.edu.au:1521:iteeo", "csse3004gg", "groupg");

            Statement stmt = con.createStatement();

            ResultSet rset = stmt.executeQuery("SELECT MAX(RESPONSE_ID) FROM RESPONSES");
            rset.next();
            int maxID = Integer.parseInt(rset.getString("MAX(RESPONSE_ID)"));

            rset = stmt.executeQuery("SELECT MAX(USER_ID) FROM USERS");
            rset.next();
            int maxUserID = Integer.parseInt(rset.getString("MAX(USER_ID)"));


            //INSERT INTO RESPONSES(response_id, feedback, answer_id, created_at, user_id, session_id)
            for (Response res : responses) {
                System.out.println("Card: " + res.getResponseCardId());



                //Check and see if we have that device registered in the database
                ResultSet uset = stmt.executeQuery(
                        "SELECT USERNAME"
                        + " FROM USERS"
                        + " WHERE USERNAME = '" + res.getResponseCardId() + "'");

                // If not get the max userid and create the user.
                if (uset.next() == false) {

                    System.out.println("INSERT INTO USERS(USER_ID, USER_TYPE, USERNAME, CREATED_AT) "
                            + "VALUES(" + maxUserID + ", -2, " + res.getResponseCardId() + ", sysdate)");
                    stmt.executeUpdate("INSERT INTO USERS(USER_ID, USER_TYPE, USERNAME, CREATED_AT) "
                            + "VALUES(" + maxUserID + ", -2, '" + res.getResponseCardId() + "', sysdate)");

                }

                uset = stmt.executeQuery(
                        "SELECT USER_ID"
                        + " FROM USERS"
                        + " WHERE USERNAME = '" + res.getResponseCardId() + "'");
                uset.next();

                //Build our string of values,
                query = query.concat("INTO RESPONSES (RESPONSE_ID, FEEDBACK, ANSWER_ID, CREATED_AT, USER_ID, SESSION_ID) "
                        + "VALUES ("
                        + (maxID+1) + ", "
                        + res.getResponse() + ", "
                        + answerID + ", "
                        + "sysdate, "
                        + uset.getString("USER_ID")
                        + ", " + sessionID
                        + ")"
                        + " \n");
                maxID++;

            }

            query = query.concat("SELECT * FROM dual");
            System.out.println("QUERY STRING FOR RESPONSES::::: \n" + query);

            rset = stmt.executeQuery(query);

            stmt.close();
            System.out.println("Ok.");
            //responses.get(0).
        } catch (Exception e) {
            System.out.println(e.getMessage());
            e.printStackTrace();
        }
    }
}
