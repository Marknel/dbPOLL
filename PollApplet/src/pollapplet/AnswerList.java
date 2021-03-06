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
public class AnswerList {

    /**
     * List of answer objects for a specific questionID
     */
    public LinkedList<dbAnswer> answers = new LinkedList();

    /**
     * Retrieves all answers in the database corresponding to a questionID.
     * Storing the answers in a Linked List.
     * 
     * @param questionID: Current polling question
     */
    public void loadAnswers(int questionID) throws SQLException {
        Connection con;
        answers = new LinkedList();
        // pull all polls for poll master into applet
        DriverManager.registerDriver(new oracle.jdbc.OracleDriver());
        con = DriverManager.getConnection("jdbc:oracle:thin:@oracle.students.itee.uq.edu.au:1521:iteeo", "csse3004gg", "groupg");

        Statement stmt = con.createStatement();
        ResultSet rset = stmt.executeQuery(
                "SELECT ANSWER_ID, ANSWER, CORRECT, WEIGHT"
                + " FROM ANSWERS A"
                + " WHERE A.QUESTION_ID = " + questionID);

        while (rset.next()) {
            int correct;
            int weight;

            if (rset.getString("CORRECT") == null) {
                correct = -1;
            } else {
                correct = Integer.parseInt(rset.getString("CORRECT"));
            }

            if (rset.getString("WEIGHT") == null) {
                weight = -1;
            } else {
                weight = Integer.parseInt(rset.getString("WEIGHT"));
            }


            answers.add(new dbAnswer(
                    Integer.parseInt(rset.getString("ANSWER_ID")),
                    rset.getString("ANSWER"),
                    correct,
                    weight));
        }

        stmt.close();
    }

    // <editor-fold defaultstate="collapsed" desc="Basic dbAnswer class fold. Getter/Setter baby code"> 
    /**
     * Basic model of a Answer in the database.
     * 
     * @author 42009432 - Adam Young
     */
    public class dbAnswer {

        private int answerID;
        private String answerText;
        private int correctAnswer;
        private int weight;

        /**
         * Constructor for basic database Answer
         * @param ansID: Answer_ID field from database
         * @param ansText: Answer field from database
         * @param correctans: Correct field from database
         * @param weight: Weight field from database
         */
        public dbAnswer(int ansID, String ansText, int correctans, int weight) {
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

}
