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
    
    static final String connect_string = 
                  //"jdbc:oracle:thin:hr/hr@//localhost:1521/orcl.oracle.com";
    "jdbc:oracle:thin:@oracle.students.itee.uq.edu.au:1521:teach, csse3004gg, groupg";
    
    Connection dbconn;
    
    /**
     * A list containing all the polls assigned to a specific Poll Master
     */
    LinkedList<Poll> polls = new LinkedList();

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
        // pull all polls for poll master into applet
    }

 // <editor-fold defaultstate="collapsed" desc="Basic Poll class fold. Getter/Setter baby code"> 
private class Poll {
    
    private int pollId;
    private String pollName;

    public Poll(int pollId) {
        this.pollId = pollId;
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
        
        p.loadPolls(4);
    }

}
