/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package pollapplet;

import pollapplet.PollApplet.*;

/**
 *
 * @author s4200943
 */

/* SQL Queries
 
 Returns Polls for specific pollmaster
 
     SELECT p.POLL_ID, p.POLL_NAME 
     FROM POLLS P
     INNER JOIN assignedpolls a
     ON p.poll_id = a.poll_id
     WHERE user_id = <masterid>;

 Should be enough to get a decent question poll started:

    SELECT QUESTION_ID, QUESTION_TYPE, QUESTION, NUM
    FROM QUESTIONS Q
    WHERE Q.POLL_ID = 2;
 
 
 
 */


public class PollActions {

    public PollList p =  new PollList();
    
    
    public PollActions()  {   
    }
    
    
    
    public void getQuestions(int pollid){
        // Retrieve questions for selected poll from database
        
        
    }
    
    public void saveQuestionResponses(int questionid, ResponseListModel r){
        //Add responses for certain question in poll to database
        
        
    }
    
    public void openQuestion(int questionId){
        //Open question and allow polling?
        
        
    }
    
    public void closeQuestion(int questionId){
        // close key and stop polling
        
    }
    
    public void nextQuestion(int questionId){
        
        
    }
    
    public void prevQuestion(int questionId){
        
        
    }
    
    
    
    
    
}
