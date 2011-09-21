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
import java.awt.Font;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import javax.swing.DefaultListModel;
import javax.swing.JOptionPane;
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
            showError("Could not initialize allReceivers.", e);
        }
         List<Receiver> allReceivers = receiverListModel.getReceivers();
        Receiver currentReceiver = allReceivers.get(0);
        currentReceiver.setChannel(22);

        dataset = createDataset();
    }
    
    private void showError(String message, Exception e) {
        JOptionPane.showMessageDialog(null, message + "\n Reason:" + e.getMessage());
    }
    
    private void initModel() {
       
        //keyPadResponseList.setModel(responseListModel);
        testingInfoLabel.setText("Please set your Clicker Device to channel 22 and  wait for testing to begin.");

       
       System.out.println("Changed!!!!");
       System.out.println(receiverListModel.getSize());
       
       //responseTable.setModel(responseTableModel);
       //responseTable.setDefaultRenderer(responseTable.getColumnClass(0), PollRenderer);
       //responseTable.setRowHeight(20);

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
            testingInfoLabel.setText("Please press Keys on your Clicker Device to ensure your responses are received");


            //responseChart.setSubtitle(evt.getActionCommand() + " Polling Open");
        } catch (Exception e) {
            showError("Unable to start poll.", e);
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
    
    public class ResponseListModel extends DefaultListModel {
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
    private String[][] data = new String[25][4];


    public int getColumnCount() {
        return columnNames.length;
    }
    
    public void addDeviceID(String deviceID, int key){
        int detectedCount = 0;
        for(int row = 0; row < 29; row++){
            for(int column = 0; column < 4; column++ ){
                if(this.getValueAt(row, column) == null){
                    this.setValueAt(deviceID+" : "+key, row, column);
                    System.out.println("Break at: "+column+" "+row);
                    detectedCount = Integer.parseInt(detectedLbl.getText());
                    detectedCount++;
                    detectedLbl.setText(""+detectedCount);
                    return;
                    
                }else if((this.getValueAt(row, column).toString().split("\\s+")[0]).compareTo(deviceID) == 0){
                    //cal colour change code here!
                    this.setValueAt(deviceID+" : "+key, row, column);
                    return;
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
                                
                        cell.setFont(new Font("Arial",Font.PLAIN, 16));
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
}
   
    /** This method is called from within the init() method to
     * initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is
     * always regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        userDialog = new javax.swing.JDialog();
        jLabel2 = new javax.swing.JLabel();
        masterIDTxt = new javax.swing.JTextField();
        submitBtn = new javax.swing.JButton();
        jLabel3 = new javax.swing.JLabel();
        pollDialog = new javax.swing.JDialog();
        MasterPanel = new javax.swing.JPanel();
        titleLabel = new javax.swing.JLabel();
        jPanel1 = new javax.swing.JPanel();
        stopButton = new javax.swing.JButton();
        startButton = new javax.swing.JButton();
        detectedInfoLabel = new javax.swing.JLabel();
        jLabel1 = new javax.swing.JLabel();
        jPanel2 = new javax.swing.JPanel();
        questionText = new javax.swing.JLabel();
        answer4Text = new javax.swing.JLabel();
        answer3Text = new javax.swing.JLabel();
        answer5Text = new javax.swing.JLabel();
        answer2Text = new javax.swing.JLabel();
        answer1Text = new javax.swing.JLabel();
        answer8Text = new javax.swing.JLabel();
        answer7Text = new javax.swing.JLabel();
        answer6Text = new javax.swing.JLabel();
        answer10Text = new javax.swing.JLabel();
        answer9Text = new javax.swing.JLabel();
        stopButton1 = new javax.swing.JButton();
        detectedLbl = new javax.swing.JLabel();
        testingInfoLabel = new javax.swing.JLabel();

        jLabel2.setText("Poll Master Id = ");

        submitBtn.setText("Submit");

        jLabel3.setFont(new java.awt.Font("Tahoma", 0, 18)); // NOI18N
        jLabel3.setText("<Used For Testing> ");

        javax.swing.GroupLayout userDialogLayout = new javax.swing.GroupLayout(userDialog.getContentPane());
        userDialog.getContentPane().setLayout(userDialogLayout);
        userDialogLayout.setHorizontalGroup(
            userDialogLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(userDialogLayout.createSequentialGroup()
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addGroup(userDialogLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(userDialogLayout.createSequentialGroup()
                        .addComponent(jLabel2)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(userDialogLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING, false)
                            .addComponent(submitBtn, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(masterIDTxt, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.PREFERRED_SIZE, 77, javax.swing.GroupLayout.PREFERRED_SIZE)))
                    .addComponent(jLabel3)))
        );
        userDialogLayout.setVerticalGroup(
            userDialogLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(userDialogLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jLabel3)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(userDialogLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel2)
                    .addComponent(masterIDTxt, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(submitBtn)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        javax.swing.GroupLayout pollDialogLayout = new javax.swing.GroupLayout(pollDialog.getContentPane());
        pollDialog.getContentPane().setLayout(pollDialogLayout);
        pollDialogLayout.setHorizontalGroup(
            pollDialogLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 400, Short.MAX_VALUE)
        );
        pollDialogLayout.setVerticalGroup(
            pollDialogLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 300, Short.MAX_VALUE)
        );

        MasterPanel.setMaximumSize(new java.awt.Dimension(800, 600));
        MasterPanel.setPreferredSize(new java.awt.Dimension(800, 600));

        titleLabel.setFont(new java.awt.Font("Tahoma", 0, 28));
        titleLabel.setText("KeyPad Polling");

        jPanel1.setBorder(new javax.swing.border.LineBorder(new java.awt.Color(0, 0, 0), 1, true));
        jPanel1.setMaximumSize(new java.awt.Dimension(800, 600));
        jPanel1.setPreferredSize(new java.awt.Dimension(800, 600));

        stopButton.setFont(new java.awt.Font("Tahoma", 0, 36));
        stopButton.setText("Next Question");
        stopButton.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                stopButtonActionPerformed(evt);
            }
        });

        startButton.setFont(new java.awt.Font("Tahoma", 0, 36));
        startButton.setText("Start Polling");
        startButton.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                startButtonActionPerformed(evt);
            }
        });

        detectedInfoLabel.setText("Devices Found");

        jLabel1.setFont(new java.awt.Font("Tahoma", 0, 24));
        jLabel1.setText("poll Text");

        jPanel2.setBackground(new java.awt.Color(255, 255, 255));
        jPanel2.setBorder(javax.swing.BorderFactory.createBevelBorder(javax.swing.border.BevelBorder.LOWERED));

        questionText.setFont(new java.awt.Font("Tahoma", 0, 48));
        questionText.setText("Question text");

        answer4Text.setFont(new java.awt.Font("Tahoma", 0, 36));
        answer4Text.setText("1. Answer");

        answer3Text.setFont(new java.awt.Font("Tahoma", 0, 36));
        answer3Text.setText("1. Answer");

        answer5Text.setFont(new java.awt.Font("Tahoma", 0, 36));
        answer5Text.setText("1. Answer");

        answer2Text.setFont(new java.awt.Font("Tahoma", 0, 36));
        answer2Text.setText("1. Answer");

        answer1Text.setFont(new java.awt.Font("Tahoma", 0, 36));
        answer1Text.setText("1. Answer");

        answer8Text.setFont(new java.awt.Font("Tahoma", 0, 36));
        answer8Text.setText("1. Answer");

        answer7Text.setFont(new java.awt.Font("Tahoma", 0, 36));
        answer7Text.setText("1. Answer");

        answer6Text.setFont(new java.awt.Font("Tahoma", 0, 36));
        answer6Text.setText("1. Answer");

        answer10Text.setFont(new java.awt.Font("Tahoma", 0, 36));
        answer10Text.setText("1. Answer");

        answer9Text.setFont(new java.awt.Font("Tahoma", 0, 36));
        answer9Text.setText("1. Answer");

        javax.swing.GroupLayout jPanel2Layout = new javax.swing.GroupLayout(jPanel2);
        jPanel2.setLayout(jPanel2Layout);
        jPanel2Layout.setHorizontalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel2Layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(answer1Text)
                    .addGroup(jPanel2Layout.createSequentialGroup()
                        .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(answer2Text)
                            .addComponent(answer3Text)
                            .addComponent(answer4Text)
                            .addComponent(answer5Text))
                        .addGap(191, 191, 191)
                        .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(answer7Text)
                            .addComponent(answer8Text)
                            .addComponent(answer6Text)
                            .addComponent(answer10Text)
                            .addComponent(answer9Text)))
                    .addComponent(questionText))
                .addContainerGap(237, Short.MAX_VALUE))
        );
        jPanel2Layout.setVerticalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel2Layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(questionText)
                .addGap(18, 18, 18)
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(jPanel2Layout.createSequentialGroup()
                        .addComponent(answer9Text)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(answer7Text)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(answer8Text)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(answer6Text)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(answer10Text))
                    .addGroup(jPanel2Layout.createSequentialGroup()
                        .addComponent(answer1Text)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(answer2Text)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(answer3Text)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(answer4Text)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(answer5Text)))
                .addContainerGap(56, Short.MAX_VALUE))
        );

        stopButton1.setFont(new java.awt.Font("Tahoma", 0, 36));
        stopButton1.setText("Prev Question");
        stopButton1.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                stopButton1ActionPerformed(evt);
            }
        });

        detectedLbl.setFont(new java.awt.Font("Tahoma", 0, 24));
        detectedLbl.setHorizontalAlignment(javax.swing.SwingConstants.TRAILING);
        detectedLbl.setText("0");

        javax.swing.GroupLayout jPanel1Layout = new javax.swing.GroupLayout(jPanel1);
        jPanel1.setLayout(jPanel1Layout);
        jPanel1Layout.setHorizontalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel1Layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jLabel1)
                .addGap(294, 294, 294)
                .addComponent(detectedLbl, javax.swing.GroupLayout.PREFERRED_SIZE, 32, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(detectedInfoLabel)
                .addGap(289, 289, 289))
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jPanel2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap(23, Short.MAX_VALUE))
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(stopButton1)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(startButton)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(stopButton)
                .addContainerGap(32, Short.MAX_VALUE))
        );
        jPanel1Layout.setVerticalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(detectedLbl)
                    .addComponent(detectedInfoLabel)
                    .addComponent(jLabel1))
                .addGap(7, 7, 7)
                .addComponent(jPanel2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(stopButton1)
                    .addComponent(startButton)
                    .addComponent(stopButton))
                .addContainerGap())
        );

        testingInfoLabel.setFont(new java.awt.Font("Tahoma", 0, 14));
        testingInfoLabel.setText("Please press Keys on your Clicker Device to ensure your responses are received");

        javax.swing.GroupLayout MasterPanelLayout = new javax.swing.GroupLayout(MasterPanel);
        MasterPanel.setLayout(MasterPanelLayout);
        MasterPanelLayout.setHorizontalGroup(
            MasterPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(MasterPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(titleLabel, javax.swing.GroupLayout.PREFERRED_SIZE, 186, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(56, 56, 56)
                .addComponent(testingInfoLabel, javax.swing.GroupLayout.PREFERRED_SIZE, 496, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap(53, Short.MAX_VALUE))
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, MasterPanelLayout.createSequentialGroup()
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addComponent(jPanel1, javax.swing.GroupLayout.PREFERRED_SIZE, 781, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );
        MasterPanelLayout.setVerticalGroup(
            MasterPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(MasterPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(MasterPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(testingInfoLabel, javax.swing.GroupLayout.PREFERRED_SIZE, 17, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(titleLabel))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(jPanel1, javax.swing.GroupLayout.DEFAULT_SIZE, 515, Short.MAX_VALUE)
                .addContainerGap())
        );

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(MasterPanel, javax.swing.GroupLayout.DEFAULT_SIZE, 801, Short.MAX_VALUE)
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(MasterPanel, javax.swing.GroupLayout.PREFERRED_SIZE, 582, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );
    }// </editor-fold>//GEN-END:initComponents

private void stopButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_stopButtonActionPerformed
try {
            poll.stop();
            testingInfoLabel.setText("Testing has finished. Please wait for instruction");
            
            //responseChart.setSubtitle("Polling Closed");
        } catch (Exception e) {
           showError("Unable to stop poll.", e);
        }
}//GEN-LAST:event_stopButtonActionPerformed

private void startButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_startButtonActionPerformed
// TODO add your handling code here:
}//GEN-LAST:event_startButtonActionPerformed

private void stopButton1ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_stopButton1ActionPerformed
// TODO add your handling code here:
}//GEN-LAST:event_stopButton1ActionPerformed

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JPanel MasterPanel;
    private javax.swing.JLabel answer10Text;
    private javax.swing.JLabel answer1Text;
    private javax.swing.JLabel answer2Text;
    private javax.swing.JLabel answer3Text;
    private javax.swing.JLabel answer4Text;
    private javax.swing.JLabel answer5Text;
    private javax.swing.JLabel answer6Text;
    private javax.swing.JLabel answer7Text;
    private javax.swing.JLabel answer8Text;
    private javax.swing.JLabel answer9Text;
    private javax.swing.JLabel detectedInfoLabel;
    private javax.swing.JLabel detectedLbl;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JPanel jPanel1;
    private javax.swing.JPanel jPanel2;
    private javax.swing.JTextField masterIDTxt;
    private javax.swing.JDialog pollDialog;
    private javax.swing.JLabel questionText;
    private javax.swing.JButton startButton;
    private javax.swing.JButton stopButton;
    private javax.swing.JButton stopButton1;
    private javax.swing.JButton submitBtn;
    private javax.swing.JLabel testingInfoLabel;
    private javax.swing.JLabel titleLabel;
    private javax.swing.JDialog userDialog;
    // End of variables declaration//GEN-END:variables
}


