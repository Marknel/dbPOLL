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
 * @author s4200943
 * 
 */
public class PollList{

    //Connection dbconn;
    
    /**
     * A list containing all the polls assigned to a specific Poll Master
     */
    public LinkedList<Poll> polls = new LinkedList();

    public PollList() {

    }

    /**
     * Runs over the database reading each poll into a Poll object and adding
     * the object to the polls list.
     * @param pollMaster must be a valid pollMaster in the database. 
     * polls List will contain null items if the poll master has not been 
     * assigned to a poll.
     */
    public void loadPolls(int pollMaster){
        Connection con;
        try {
            // pull all polls for poll master into applet
            DriverManager.registerDriver(new oracle.jdbc.OracleDriver());
            con = DriverManager.getConnection("jdbc:oracle:thin:@oracle.students.itee.uq.edu.au:1521:iteeo", "csse3004gg", "groupg");
            
            Statement stmt = con.createStatement();
            ResultSet rset = stmt.executeQuery(
            
             "SELECT p.POLL_ID, p.POLL_NAME"
             +" FROM POLLS P"
             +" INNER JOIN assignedpolls a"
             +" ON p.poll_id = a.poll_id"
             +" WHERE user_id = "+pollMaster
                    );
            
            while (rset.next()) {
                polls.add(new Poll(Integer.parseInt(rset.getString("POLL_ID")), rset.getString("POLL_NAME")));
            }
            
             for (Poll p : polls){
                System.out.println("POLL ID: "+p.getPollId()+ " Name: " +p.getPollName()); 
                 
             }
            
            stmt.close();
            System.out.println ("Ok.");
            
            
        } catch (SQLException ex) {
            Logger.getLogger(PollList.class.getName()).log(Level.SEVERE, null, ex);
        }
        
        
    }

 // <editor-fold defaultstate="collapsed" desc="Basic Poll class fold. Getter/Setter baby code"> 
public class Poll {
    
    private int pollId;
    private String pollName;

    public Poll(int pollId, String pollName) {
        this.pollId = pollId;
        this.pollName = pollName;
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
}
//</editor-fold> 


public static void main(String [] args)
    {
        PollList p = new PollList();
        
        p.loadPolls(5);
    }

}
