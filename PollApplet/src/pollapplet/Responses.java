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
 * @author s42009432 - Adam Young
 * 
 * Provides means of storing Responses in the database
 */
public class Responses {

    /**
     * Provides a means of accessing Answers for a selected question in order
     * to extract answerID + Answer for response record insertion.
     */
    private AnswerList Answers = new AnswerList();

    /**
     * Default blank constructor to call methods from class
     */
    Responses() {
    }

    /**
     * Formats and inserts given response data into the database.
     * If keypad users have not being provided with user accounts a default
     * will be generated.
     * 
     * @param responses: List of response objects gathered from the receiver.
     * @param sessionID: The current session being polled.
     * @param questionID: The current question for which the responses belong.
     */
    public void saveResponses(List<Response> responses, int sessionID, int questionID) throws SQLException {

        Answers.loadAnswers(questionID);

        Connection con;
        String query = "INSERT ALL\n";
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
                        + (maxID + 1) + ", '"
                        + Answers.answers.get(Integer.parseInt(res.getResponse()) - 1).getAnswerText() + "', "
                        + Answers.answers.get(Integer.parseInt(res.getResponse()) - 1).getAnswerID() + ", "
                        + "sysdate, "
                        + uset.getString("USER_ID")
                        + ", " + sessionID
                        + ")"
                        + " \n");
                maxID++;

            }

            query = query.concat("SELECT * FROM dual");
            System.out.println("---- QUERY STRING FOR RESPONSES ---- \n" + query +"\n ------------------------");

            rset = stmt.executeQuery(query);

            stmt.close();
            System.out.println("Ok.");
            //responses.get(0).
    }
}
