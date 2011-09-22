/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package pollapplet;

import java.sql.*;
import java.util.LinkedList;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author s4200943
 */
public class AnswerList {
    
    public LinkedList<dbAnswer> answers = new LinkedList();
    
    
    public void loadAnswers(int questionID) {
        Connection con;
        try {
            // pull all polls for poll master into applet
            DriverManager.registerDriver(new oracle.jdbc.OracleDriver());
            con = DriverManager.getConnection("jdbc:oracle:thin:@oracle.students.itee.uq.edu.au:1521:iteeo", "csse3004gg", "groupg");
            
            Statement stmt = con.createStatement();
            ResultSet rset = stmt.executeQuery(
             "SELECT ANSWER_ID, ANSWER, CORRECT, WEIGHT"+
             " FROM ANSWERS A"+
             " WHERE A.QUESTION_ID = "+questionID);      

            while (rset.next()) {
                int correct;
                int weight;
                        
                if(rset.getString("CORRECT") == null){
                    correct = -1;
                }else{
                    correct = Integer.parseInt(rset.getString("CORRECT"));
                }
                        
                if(rset.getString("WEIGHT") == null){
                    weight = -1;
                }else{
                    weight = Integer.parseInt(rset.getString("WEIGHT"));
                }
                
                
                answers.add(new dbAnswer(
                        Integer.parseInt(rset.getString("ANSWER_ID")),
                        rset.getString("ANSWER"),
                        correct,
                        weight
                        ));
            }
            
             for (dbAnswer a : answers){
                System.out.println(
                        "Answer ID: "+a.getAnswerID()+ 
                        " Text: " +a.getAnswerText() +
                        " Correct: "+ a.getCorrectAnswer()+
                        " Weight: "+a.getWeight());  
             }
            
            stmt.close();
            System.out.println ("Ok.");
            
            
        } catch (SQLException ex) {
            Logger.getLogger(PollList.class.getName()).log(Level.SEVERE, null, ex);
        }
        
        
    }
    
    
    // <editor-fold defaultstate="collapsed" desc="Basic dbAnswer class fold. Getter/Setter baby code"> 
    public class dbAnswer{
        
        private int answerID;
        private String answerText;
        private int correctAnswer;
        private int weight;
        
        public dbAnswer(int ansID, String ansText, int correctans, int weight){
               this.answerID = ansID;
               this.answerText = ansText;
               this.correctAnswer = correctans;
               this.weight = weight;
        }
        
        public String getAnswerText() {
            return answerText;
        }

        public void setAnswerText(String AnswerText) {
            this.answerText = AnswerText;
        }

        public int getCorrectAnswer() {
            return correctAnswer;
        }

        public void setCorrectAnswer(int CorrectAnswer) {
            this.correctAnswer = CorrectAnswer;
        }

        public int getAnswerID() {
            return answerID;
        }

        public void setAnswerID(int answerID) {
            this.answerID = answerID;
        }

        public int getWeight() {
            return weight;
        }

        public void setWeight(int weight) {
            this.weight = weight;
        }
  
    }
    //</editor-fold>
    
    public static void main(String [] args)
    {
        AnswerList a = new AnswerList();
        
        a.loadAnswers(3);
    }
}
