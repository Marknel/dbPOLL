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
public class QuestionList {
    
    public LinkedList<dbQuestion> questions = new LinkedList();
    
    public QuestionList() {
    }

    public void loadQuestions(int pollID) {
        Connection con;
        try {
            // pull all polls for poll master into applet
            DriverManager.registerDriver(new oracle.jdbc.OracleDriver());
            con = DriverManager.getConnection("jdbc:oracle:thin:@oracle.students.itee.uq.edu.au:1521:iteeo", "csse3004gg", "groupg");
            
            Statement stmt = con.createStatement();
            ResultSet rset = stmt.executeQuery(
             "SELECT QUESTION_ID, QUESTION_TYPE, QUESTION, CHART_STYLE, SHORT_ANSWER_TYPE, NUM"+
             " FROM QUESTIONS Q"+
             " WHERE Q.POLL_ID = "+pollID);      

            while (rset.next()) {
                int shorttype; 
                        
                if(rset.getString("SHORT_ANSWER_TYPE") == null){
                    shorttype = -1;
                }else{
                    shorttype = Integer.parseInt(rset.getString("SHORT_ANSWER_TYPE"));
                }
                
                
                questions.add(new dbQuestion(
                        Integer.parseInt(rset.getString("QUESTION_ID")), 
                        Integer.parseInt(rset.getString("QUESTION_TYPE")),
                        rset.getString("QUESTION"), 
                        Integer.parseInt(rset.getString("CHART_STYLE")), 
                        shorttype, 
                        Integer.parseInt(rset.getString("NUM"))));
            }
            
             for (dbQuestion q : questions){
                System.out.println(
                        "Question ID: "+q.getQuestionID()+ 
                        " Type: " +q.getQuestionType() +
                        " Question: "+ q.getQuestionText()+
                        " Chart Style: "+q.getChartStyle()+
                        " Short Answer Type: "+q.getShortAnswerType()+
                        " Num in seq: " + q.getNumInSequence());  
             }
            
            stmt.close();
            System.out.println ("Ok.");
            
            
        } catch (SQLException ex) {
            Logger.getLogger(PollList.class.getName()).log(Level.SEVERE, null, ex);
        }
        
        
    }

// <editor-fold defaultstate="collapsed" desc="Basic dbQuestion class fold. Getter/Setter baby code"> 
public class dbQuestion {
    
    private int questionID;
    private int questionType;
    private String questionText; 
    private int chartStyle;
    private int shortAnswerType;
    private int numInSequence;

    public dbQuestion(int questionId, int questionType, String question, int chartStyle, int shortAnswerType, int numInSequence) {
        this.questionID = questionId;
        this.questionType = questionType;
        this.questionText = question;
        this.chartStyle = chartStyle;
        this.shortAnswerType = shortAnswerType;
        this.numInSequence = numInSequence;
    }

        public int getChartStyle() {
            return chartStyle;
        }

        public void setChartStyle(int chartStyle) {
            this.chartStyle = chartStyle;
        }

        public int getNumInSequence() {
            return numInSequence;
        }

        public void setNumInSequence(int numInSequence) {
            this.numInSequence = numInSequence;
        }

        public int getQuestionID() {
            return questionID;
        }

        public void setQuestionID(int questionID) {
            this.questionID = questionID;
        }

        public String getQuestionText() {
            return questionText;
        }

        public void setQuestionText(String questionText) {
            this.questionText = questionText;
        }

        public int getQuestionType() {
            return questionType;
        }

        public void setQuestionType(int questionType) {
            this.questionType = questionType;
        }

        public int getShortAnswerType() {
            return shortAnswerType;
        }

        public void setShortAnswerType(int shortAnswerType) {
            this.shortAnswerType = shortAnswerType;
        }
    }
//</editor-fold>  



public static void main(String [] args)
    {
        QuestionList q = new QuestionList();
        
        q.loadQuestions(5);
    }
}