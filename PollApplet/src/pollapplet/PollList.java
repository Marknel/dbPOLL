/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package pollapplet;

import java.sql.*;
import java.util.logging.Level;
import java.util.logging.Logger;
import oracle.jdbc.pool.OracleDataSource;

import java.util.LinkedList;
import javax.swing.DefaultListModel;

/**
 * Contains methods to allow the getting of poll data from database.
 * @author 42009432 - Adam Young
 * 
 */
public class PollList {

    //Connection dbconn;
    /**
     * A list containing all the polls assigned to a specific dbPoll Master
     */
    public LinkedList<dbPoll> polls = new LinkedList();

    public PollList() {
    }

    /**
     * Runs over the database reading each poll into a dbPoll object and adding
     * the object to the polls list.
     * 
     * @param pollMaster must be a valid pollMaster in the database. 
     * polls List will contain null items if the poll master has not been 
     * assigned to a poll.
     */
    public void loadPolls(int pollMaster) {
        Connection con;
        polls = new LinkedList();
        try {
            // pull all polls for poll master into applet
            DriverManager.registerDriver(new oracle.jdbc.OracleDriver());
            con = DriverManager.getConnection("jdbc:oracle:thin:@oracle.students.itee.uq.edu.au:1521:iteeo", "csse3004gg", "groupg");

            Statement stmt = con.createStatement();
            ResultSet rset = stmt.executeQuery(
                    "SELECT p.POLL_ID, s.SESSION_ID, p.POLL_NAME, s.SESSION_NAME"
                    + " FROM POLLS P"
                    + " INNER JOIN assignedpolls a"
                    + " ON p.poll_id = a.poll_id"
                    + " INNER JOIN sessions s"
                    + " ON p.poll_id = s.poll_id"
                    + " WHERE user_id = " + pollMaster);

            while (rset.next()) {
                polls.add(new dbPoll(Integer.parseInt(rset.getString("POLL_ID")), Integer.parseInt(rset.getString("SESSION_ID")), rset.getString("POLL_NAME"), rset.getString("SESSION_NAME")));

            }

            /*for (dbPoll p : polls) {
            System.out.println("POLL ID: " + p.getPollId() + " Name: " + p.getPollName()+ " Session: "+p.getPollSession());
            
            }*/

            stmt.close();
            //System.out.println("Ok.");


        } catch (SQLException ex) {
            Logger.getLogger(PollList.class.getName()).log(Level.SEVERE, null, ex);
        }


    }

    // <editor-fold defaultstate="collapsed" desc="Basic dbPoll class fold. Getter/Setter baby code"> 
    public class dbPoll {

        private int pollId;
        private int sessionId;
        private String pollName;
        private String pollSession;

        public dbPoll(int pollId, int sessionId, String pollName, String pollSession) {
            this.pollId = pollId;
            this.pollName = pollName;
            this.pollSession = pollSession;
            this.sessionId = sessionId;
        }

        public int getPollId() {
            return pollId;
        }

        public void setPollId(int pollId) {
            this.pollId = pollId;
        }

        public String getPollName() {
            return pollName;
        }

        public void setPollName(String pollName) {
            this.pollName = pollName;
        }

        public String getPollSession() {
            return pollSession;
        }

        public void setPollSession(String pollSession) {
            this.pollSession = pollSession;
        }

        public int getSessionId() {
            return sessionId;
        }

        public void setSessionId(int sessionId) {
            this.sessionId = sessionId;
        }

        /**
         * Formatted poll - session pair
         * 
         * @return 'pollName - pollSession'
         */
        @Override
        public String toString() {

            return this.pollName + " - " + this.pollSession;
        }
    }
//</editor-fold> 
}
