/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/*
 * PollApplet.java
 *
 * Created on 17/08/2011, 5:34:40 PM
 */
package pollapplet;

import com.turningtech.poll.Poll;
import com.turningtech.poll.PollService;
import com.turningtech.poll.Response;
import com.turningtech.poll.ResponseListener;
import com.turningtech.test.Test;
import com.turningtech.test.DeviceWakeup;
import com.turningtech.test.Examination;
import com.turningtech.test.Question;
import com.turningtech.test.TestService;
import com.turningtech.test.ResponseTest;
import com.turningtech.test.ResponseListenerTest; 
import com.turningtech.receiver.Receiver;
import com.turningtech.receiver.ReceiverService;
import com.turningtech.receiver.ResponseCardLibrary;
import java.awt.Color;
import java.awt.Component;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import javax.swing.DefaultListModel;
import javax.swing.JTable;
import javax.swing.table.AbstractTableModel;
import javax.swing.table.DefaultTableCellRenderer;
import javax.swing.table.DefaultTableModel;
import org.jfree.data.category.DefaultCategoryDataset;

/**
 *
 * @author s4200943
 */
public class PollApplet extends javax.swing.JApplet {
    private Poll poll;
    private ResponseListModel responseListModel = new ResponseListModel();
    private ReceiverListModel receiverListModel = new ReceiverListModel();
    private DefaultCategoryDataset dataset;
    private ReceiverTableModel responseTableModel = new ReceiverTableModel();
    private PollCellRenderer PollRenderer = new PollCellRenderer();
    public int chosenRow = 0;
    public int chosenColumn = 0;
    
    
    /** Initializes the applet PollApplet */
    @Override
    public void init() {
        /* Set the Nimbus look and feel */
        //<editor-fold defaultstate="collapsed" desc=" Look and feel setting code (optional) ">
        /* If Nimbus (introduced in Java SE 6) is not available, stay with the default look and feel.
         * For details see http://download.oracle.com/javase/tutorial/uiswing/lookandfeel/plaf.html 
         */
        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Windows".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (ClassNotFoundException ex) {
            java.util.logging.Logger.getLogger(PollApplet.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(PollApplet.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(PollApplet.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(PollApplet.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the applet */
        try {
            java.awt.EventQueue.invokeAndWait(new Runnable() {

                @Override
                public void run() {
                    ResponseCardLibrary.initializeLicense("University of Queensland", "24137BBFEEEA9C7F5D65B2432F10F960");
                    initReceivers();
                    initComponents();
                    initModel();
                    startButton.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                startPollHandler(evt);
            }
        });
                }
            });
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }
    
    private void initReceivers() {
        try {
            receiverListModel.addAll(ReceiverService.findReceivers());
        } catch (Exception e) {
            //showError("Could not initialize receivers.", e);
        }
        dataset = createDataset();
    }
    
    private void initModel() {
        //responseChart.setDataset(dataset);

        //ReceiverTable.setc
       
        keyPadResponseList.setModel(responseListModel);

       //ReceiverTable.setModel(recModel);
       //ReceiverTable.setValueAt("Hello", 1, 1);
       System.out.println("Changed!!!!");
       System.out.println(receiverListModel.getSize());
       
       responseTable.setModel(responseTableModel);
       responseTable.setDefaultRenderer(responseTable.getColumnClass(0), PollRenderer);
       /*responseTableModel.addDeviceID("hello",1);
       responseTableModel.addDeviceID("h",1);
       responseTableModel.addDeviceID("he",1);
       responseTableModel.addDeviceID("234l",1);
       responseTableModel.addDeviceID("h234l",2);
       responseTableModel.addDeviceID("h532el",3);
       responseTableModel.addDeviceID("h235l",4);
       responseTableModel.addDeviceID("h25l",5);
       responseTableModel.addDeviceID("h235l",6);
       responseTableModel.addDeviceID("h235l",7);
       responseTableModel.addDeviceID("h325l",8);
       responseTableModel.addDeviceID("h325l",2);*/
       
       
        
        //receiverList.setModel(receiverListModel);

        /*receiverList.getSelectionModel().addListSelectionListener(new ListSelectionListener() {

            public void valueChanged(ListSelectionEvent e) {
                if (e.getValueIsAdjusting()) {
                    return;
                }
                int selection = receiverList.getSelectedIndex();
                try {
                    Receiver receiver = receiverListModel.get(selection);
                    lblChannel.setText(Integer.toString(receiver.getChannel()));
                    lblDescription.setText(receiver.getDescription());
                    lblId.setText(receiver.getId());
                    lblId1.setText(receiver.getVersion());
                } catch (Exception ex) {
                    //ignore
                }

            }
        });
        receiverList.setCellRenderer(receiverCellRenderer);*/
    }
    
    private void startPollHandler(java.awt.event.ActionEvent evt) {                                  
        //this.responseChart.setDataset((dataset = createDataset()));
        this.responseListModel.clear();
        try {//System.out.println("evt paramString: " + evt.getActionCommand());
            //see if it's an invalid response poll that we're starting
            if (evt.getActionCommand().equals("InvalidResponse")) {
                String answerRange = "246809";
                poll = PollService.createCorrectPoll(evt.getActionCommand(), answerRange);          
            }else{
                poll = PollService.createPoll();                
            }
            poll.addResponseListener(new BasicResponseListener());
            Poll.PollingMode pollingMode = (Poll.PollingMode.SingleResponse_Numeric);
            poll.start(pollingMode);


            //responseChart.setSubtitle(evt.getActionCommand() + " Polling Open");
        } catch (Exception e) {
           // showError("Unable to start poll.", e);
        }
    }                                  
    
    private class ReceiverListModel extends DefaultListModel {

        private List<Receiver> receivers = new ArrayList();

        @Override
        public void clear() {
            receivers.clear();
            fireContentsChanged(this, 0, 0);
        }

        public void addAll(Collection newData) {
            receivers.addAll(newData);
            fireContentsChanged(this, 0, 0);
        }

        @Override
        public int getSize() {
            return receivers.size();
        }

        @Override
        public Object getElementAt(int index) {
            return receivers.get(index);
        }

        @Override
        public Receiver get(int index) {
            return receivers.get(index);
        }

        public List<Receiver> getReceivers() {
            return receivers;
        }
    }
    
    private class ResponseListModel extends DefaultListModel {
        List<Response> responses = new ArrayList();
        List<ArrayList> TestList = new ArrayList();

        @Override
        public void clear() {
            responses.clear();
            fireContentsChanged(this, 0, 0);
        }

        public void add(Response newData) {
            
            responses.add(newData);
            responseTableModel.addDeviceID(newData.getResponseCardId(),Integer.parseInt(newData.getResponse()));
            System.out.println("device: '"+newData.getResponseCardId()+"' Response: '"+newData.getResponse()+"'");
            
            fireContentsChanged(this, getSize()-2, getSize()-1);
        }

        @Override
        public int getSize() {
            return responses.size();
        }

        @Override
        public Object getElementAt(int index) {
            return responses.get(index);
        }
    }
    
    private DefaultCategoryDataset createDataset() {
        DefaultCategoryDataset newDataset = new DefaultCategoryDataset();
        List<Receiver> receivers = receiverListModel.getReceivers();
        for (Receiver receiver : receivers) {
            for (int i = 0; i < 10; i++) {
                newDataset.addValue(0, receiver.getId(), Integer.toString(i));
            }
        }

        return newDataset;

    }
    
    private class BasicResponseListener implements ResponseListener {

        public BasicResponseListener() {
        }

        public void responseReceived(Response response) {            
            responseListModel.add(response);            
            if (response.getReceiverId() == null ||
                    response.getResponse() == null ||
                    response.getResponse().equals("?")) {
                return;
            }

//            if (!dataset.getColumnKeys().contains(response.getResponse()))
                
            /**
            Number count = dataset.getValue(response.getReceiverId(), response.getResponse());
            if (count == null) {
                count = new Integer(1);
            }
            */
            //dataset.incrementValue(1, response.getReceiverId(), response.getResponse());
        }
    }
    
    class ReceiverTableModel extends AbstractTableModel {
    private String[] columnNames = {"Receivers","Receivers","Receivers","Receivers"};
    private String[][] data = new String[29][4];


    public int getColumnCount() {
        return columnNames.length;
    }
    
    public void addDeviceID(String deviceID, int key){
        int flag = 0;
        for(int row = 0; row < 29; row++){
            if(flag == 1){break;}
            for(int column = 0; column < 4; column++ ){
                if(this.getValueAt(row, column) == null){
                    this.setValueAt(deviceID+" : "+key, row, column);
                    System.out.println("Break at: "+column+" "+row);
                    flag = 1;
                    break;
                    
                }else if((this.getValueAt(row, column).toString().split("\\s+")[0]).compareTo(deviceID) == 0){
                    //cal colour change code here!
                    this.setValueAt(deviceID+" : "+key, row, column);
                    chosenRow = row;
                    chosenColumn = column;
                    
                   //Component c = PollRenderer.getTableCellRendererComponent(responseTable, this.getValueAt(row, column), rootPaneCheckingEnabled, rootPaneCheckingEnabled, row, column);
                   //PollRenderer.changecolor(c , row, column, key);
                    flag = 1;
                    //responseTable.repaint();
                    break;
                }
            }
        }         
    }

    public int getRowCount() {
        return data.length;
    }

    public String getColumnName(int col) {
        return columnNames[col];
    }

    public Object getValueAt(int row, int col) {
        return data[row][col];
    }

    /*
     * Don't need to implement this method unless your table's
     * data can change.
     */
    public void setValueAt(String value, int row, int col) {
        data[row][col] = value;
        fireTableCellUpdated(row, col);
    }
}
    
    public class PollCellRenderer
       extends DefaultTableCellRenderer {
        private Color[] cellColor = {
                                    Color.getHSBColor((float)0.72, (float)0.5, (float)0.95),
                                    Color.getHSBColor((float)0.48, 1, 1),
                                    Color.getHSBColor((float)2, (float)0.68, 1),
                                    Color.getHSBColor((float)29, (float)0.31, 1),
                                    Color.getHSBColor((float)0.61, (float)0.51, 1),
                                    Color.getHSBColor((float)246, (float)0.51, 1),
                                    Color.getHSBColor((float)1, (float)0.12, 1),
                                    Color.getHSBColor((float)0.32, (float)0.56, (float)0.99),
                                    Color.getHSBColor((float)148, (float)0.8, 100),
                                    Color.getHSBColor((float)305, (float)0.32, 1)
                                   };
  public Component getTableCellRendererComponent(JTable table,Object value,
                   boolean isSelected, boolean hasFocus, int row, int column) {
   int keyPress;     
   Component cell = super.getTableCellRendererComponent(
                               table, value, isSelected, hasFocus, row, column);

                        
	   		if( value != null )
	   		{
                            System.out.println("Key Clicked: "+value.toString().split("\\s+")[2]);
                            keyPress = Integer.parseInt(value.toString().split("\\s+")[2]);
	   			cell.setBackground(cellColor[keyPress]);
	   			// you can also customize the Font and Foreground this way
	   			// cell.setForeground();
	   			// cell.setFont();
	   		}
	   		else
	   		{	
				cell.setBackground( Color.white );
	   		}
	
		return cell;   
  }
    
    
    

    // Only for specific cell
  void changecolor(Component c , int row, int column, int key){
       //c.getComponentAt(row, column).setBackground(cellColor[key]);
       c.setBackground(cellColor[key]);
       //System.out.println("Color '"+cellColor[key]+"' Break at: Column: '"+column+"' Row: '"+row+"' Key: '"+key+"'");
       c.repaint();
  }
}
   
    /** This method is called from within the init() method to
     * initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is
     * always regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jPanel1 = new javax.swing.JPanel();
        jLabel1 = new javax.swing.JLabel();
        jLabel2 = new javax.swing.JLabel();
        jLabel3 = new javax.swing.JLabel();
        AnsweredLbl = new javax.swing.JLabel();
        detectedLbl = new javax.swing.JLabel();
        jScrollPane1 = new javax.swing.JScrollPane();
        keyPadResponseList = new javax.swing.JList();
        startButton = new javax.swing.JButton();
        stopButton = new javax.swing.JButton();
        jScrollPane2 = new javax.swing.JScrollPane();
        responseTable = new javax.swing.JTable();
        jLabel4 = new javax.swing.JLabel();

        jPanel1.setPreferredSize(new java.awt.Dimension(800, 600));

        jLabel1.setFont(new java.awt.Font("Tahoma", 0, 36)); // NOI18N
        jLabel1.setText("Testing");

        jLabel2.setLabelFor(AnsweredLbl);
        jLabel2.setText("Tested");

        jLabel3.setText("Detected");

        AnsweredLbl.setFont(new java.awt.Font("Tahoma", 0, 24)); // NOI18N
        AnsweredLbl.setText("0");

        detectedLbl.setFont(new java.awt.Font("Tahoma", 0, 24)); // NOI18N
        detectedLbl.setText("0");

        jScrollPane1.setViewportView(keyPadResponseList);

        startButton.setText("Start");

        stopButton.setText("Stop");
        stopButton.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                stopButtonActionPerformed(evt);
            }
        });

        responseTable.setModel(responseTableModel);
        jScrollPane2.setViewportView(responseTable);

        jLabel4.setFont(new java.awt.Font("Tahoma", 0, 14)); // NOI18N
        jLabel4.setText("Please press Keys 1 - 0 in any order to test your Clicker Device");

        javax.swing.GroupLayout jPanel1Layout = new javax.swing.GroupLayout(jPanel1);
        jPanel1.setLayout(jPanel1Layout);
        jPanel1Layout.setHorizontalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(jPanel1Layout.createSequentialGroup()
                        .addComponent(jLabel1, javax.swing.GroupLayout.PREFERRED_SIZE, 134, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(33, 33, 33)
                        .addComponent(jLabel4)
                        .addGap(56, 56, 56)
                        .addComponent(detectedLbl)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(jLabel3)
                        .addGap(18, 18, 18)
                        .addComponent(AnsweredLbl)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(jLabel2))
                    .addGroup(jPanel1Layout.createSequentialGroup()
                        .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(startButton)
                            .addComponent(stopButton))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 48, Short.MAX_VALUE)
                        .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 182, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(jScrollPane2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(37, 37, 37)))
                .addContainerGap())
        );
        jPanel1Layout.setVerticalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(jPanel1Layout.createSequentialGroup()
                        .addContainerGap()
                        .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel1)
                            .addComponent(detectedLbl, javax.swing.GroupLayout.PREFERRED_SIZE, 41, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(AnsweredLbl, javax.swing.GroupLayout.PREFERRED_SIZE, 41, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(jLabel2)
                            .addComponent(jLabel4)))
                    .addGroup(jPanel1Layout.createSequentialGroup()
                        .addGap(38, 38, 38)
                        .addComponent(jLabel3)))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(jPanel1Layout.createSequentialGroup()
                        .addGap(44, 44, 44)
                        .addComponent(startButton)
                        .addGap(18, 18, 18)
                        .addComponent(stopButton))
                    .addGroup(jPanel1Layout.createSequentialGroup()
                        .addGap(18, 18, 18)
                        .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(jScrollPane2, javax.swing.GroupLayout.DEFAULT_SIZE, 513, Short.MAX_VALUE)
                            .addComponent(jScrollPane1, javax.swing.GroupLayout.DEFAULT_SIZE, 513, Short.MAX_VALUE))))
                .addContainerGap())
        );

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addComponent(jPanel1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                .addComponent(jPanel1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );
    }// </editor-fold>//GEN-END:initComponents

private void stopButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_stopButtonActionPerformed
try {
            poll.stop();
            
            //responseChart.setSubtitle("Polling Closed");
        } catch (Exception e) {
           // showError("Unable to stop poll.", e);
        }
}//GEN-LAST:event_stopButtonActionPerformed

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JLabel AnsweredLbl;
    private javax.swing.JLabel detectedLbl;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JLabel jLabel4;
    private javax.swing.JPanel jPanel1;
    private javax.swing.JScrollPane jScrollPane1;
    private javax.swing.JScrollPane jScrollPane2;
    private javax.swing.JList keyPadResponseList;
    private javax.swing.JTable responseTable;
    private javax.swing.JButton startButton;
    private javax.swing.JButton stopButton;
    // End of variables declaration//GEN-END:variables
}


