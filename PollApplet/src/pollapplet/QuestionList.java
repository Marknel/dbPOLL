/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package pollapplet;

import java.sql.*;
import java.util.LinkedList;

/**
 *
 * @author 42009432 - Adam Young
 */
public class QuestionList {

    public LinkedList<dbQuestion> questions = new LinkedList();

    public QuestionList() {
    }

    public void loadQuestions(int pollID) throws SQLException {
        Connection con;
        questions = new LinkedList();
            // pull all polls for poll master into applet
            DriverManager.registerDriver(new oracle.jdbc.OracleDriver());
            con = DriverManager.getConnection("jdbc:oracle:thin:@oracle.students.itee.uq.edu.au:1521:iteeo", "csse3004gg", "groupg");

            Statement stmt = con.createStatement();
            ResultSet rset = stmt.executeQuery(
                    "SELECT QUESTION_ID, QUESTION_TYPE, QUESTION, CHART_STYLE, SHORT_ANSWER_TYPE, NUM, NUMBER_OF_RESPONSES"
                    + " FROM QUESTIONS Q"
                    + " WHERE Q.POLL_ID = " + pollID);

            while (rset.next()) {
                int shorttype;
                int numResp;

                if (rset.getString("SHORT_ANSWER_TYPE") == null) {
                    shorttype = -1;
                } else {
                    shorttype = Integer.parseInt(rset.getString("SHORT_ANSWER_TYPE"));
                }

                if (rset.getString("NUMBER_OF_RESPONSES") == null) {
                    numResp = -1;
                } else {
                    numResp = Integer.parseInt(rset.getString("NUMBER_OF_RESPONSES"));
                }

                questions.add(new dbQuestion(
                        Integer.parseInt(rset.getString("QUESTION_ID")),
                        Integer.parseInt(rset.getString("QUESTION_TYPE")),
                        rset.getString("QUESTION"),
                        Integer.parseInt(rset.getString("CHART_STYLE")),
                        shorttype,
                        Integer.parseInt(rset.getString("NUM")),
                        numResp));
            }
            stmt.close();
    }

    
    public void loadTestQuestion(int QuestionID) throws SQLException {
        Connection con;
        questions = new LinkedList();
            // pull all polls for poll master into applet
            DriverManager.registerDriver(new oracle.jdbc.OracleDriver());
            con = DriverManager.getConnection("jdbc:oracle:thin:@oracle.students.itee.uq.edu.au:1521:iteeo", "csse3004gg", "groupg");

            Statement stmt = con.createStatement();
            ResultSet rset = stmt.executeQuery(
                    "SELECT QUESTION_ID, QUESTION_TYPE, QUESTION, CHART_STYLE, SHORT_ANSWER_TYPE, NUM, NUMBER_OF_RESPONSES"
                    + " FROM QUESTIONS Q"
                    + " WHERE Q.QUESTION_ID = " + QuestionID);

            while (rset.next()) {
                int shorttype;
                int numResp;

                if (rset.getString("SHORT_ANSWER_TYPE") == null) {
                    shorttype = -1;
                } else {
                    shorttype = Integer.parseInt(rset.getString("SHORT_ANSWER_TYPE"));
                }

                if (rset.getString("NUMBER_OF_RESPONSES") == null) {
                    numResp = -1;
                } else {
                    numResp = Integer.parseInt(rset.getString("NUMBER_OF_RESPONSES"));
                }

                questions.add(new dbQuestion(
                        Integer.parseInt(rset.getString("QUESTION_ID")),
                        Integer.parseInt(rset.getString("QUESTION_TYPE")),
                        rset.getString("QUESTION"),
                        Integer.parseInt(rset.getString("CHART_STYLE")),
                        shorttype,
                        Integer.parseInt(rset.getString("NUM")),
                        numResp));
            }
            stmt.close();
    }
    
    /**
     * Sets the database next question value so that synchronous polling can be
     * performed between applet and web interface.
     * @param SessionID: Current polling session id
     * @param NextQuestionID: ID of next question to poll
     * @throws SQLException on database error
     */
    public void setNextQuestion(int SessionID, int NextQuestionID) throws SQLException {
        Connection con;
        // pull all polls for poll master into applet
        DriverManager.registerDriver(new oracle.jdbc.OracleDriver());
        con = DriverManager.getConnection(
                "jdbc:oracle:thin:@oracle.students.itee.uq.edu.au:1521:iteeo",
                "csse3004gg", "groupg");

        Statement stmt = con.createStatement();
        stmt.executeUpdate(
                "UPDATE SESSIONS"
                + " SET NEXT_QUESTION = " + NextQuestionID
                + " WHERE SESSION_ID = " + SessionID);
        stmt.close();
    }

    public void openQuestion(int SessionID, int QuestionID) throws SQLException {
        Connection con;
        // pull all polls for poll master into applet
        DriverManager.registerDriver(new oracle.jdbc.OracleDriver());
        con = DriverManager.getConnection(
                "jdbc:oracle:thin:@oracle.students.itee.uq.edu.au:1521:iteeo",
                "csse3004gg", "groupg");

        Statement stmt = con.createStatement();
        stmt.executeUpdate(
                "DELETE FROM CLOSED_QUESTIONS"
                + " WHERE SESSION_ID = " + SessionID
                + " AND QUESTION_ID = " + SessionID);
        stmt.close();
    }

    public void closeQuestion(int SessionID, int QuestionID) throws SQLException {
        Connection con;
        // pull all polls for poll master into applet
        DriverManager.registerDriver(new oracle.jdbc.OracleDriver());
        con = DriverManager.getConnection(
                "jdbc:oracle:thin:@oracle.students.itee.uq.edu.au:1521:iteeo",
                "csse3004gg", "groupg");

        Statement stmt = con.createStatement();
        stmt.executeUpdate(
                "INSERT INTO CLOSED_QUESTIONS"
                + " VALUES(" + SessionID + ", " + QuestionID + ")");
        stmt.close();
    }

// <editor-fold defaultstate="collapsed" desc="Basic dbQuestion class fold. Getter/Setter baby code"> 
    public class dbQuestion {

        private int questionID;
        private int questionType;
        private String questionText;
        private int chartStyle;
        private int shortAnswerType;
        private int numInSequence;
        private int numPossibleAnswers;

        public dbQuestion(int questionId, int questionType, String question, int chartStyle, int shortAnswerType, int numInSequence, int numberOfResponses) {
            this.questionID = questionId;
            this.questionType = questionType;
            this.questionText = question;
            this.chartStyle = chartStyle;
            this.shortAnswerType = shortAnswerType;
            this.numInSequence = numInSequence;
            this.numPossibleAnswers = numberOfResponses;
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

        public int getNumPossibleAnswers() {
            return numPossibleAnswers;
        }

        public void setNumPossibleAnswers(int numPossibleAnswers) {
            this.numPossibleAnswers = numPossibleAnswers;
        }
    }
//</editor-fold>  
}