/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package javafxapplicationtest;

import java.net.URL;
import java.util.List;
import java.util.ResourceBundle;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javax.ws.rs.client.Client;
import javax.ws.rs.client.ClientBuilder;
import javax.ws.rs.client.WebTarget;
import javax.ws.rs.core.GenericType;

/**
 *
 * @author felipe
 */
public class TestController implements Initializable {
    
    @FXML
    private Label label;
    @FXML
    private TextField textFieldSearch;
    @FXML
    private Button buttonSearch;
    @FXML
    private TableView<Customer> tableView;
    
    
    @FXML
    private void handleSearchAction() {
        WebTarget clientTarget;
        ObservableList<Customer> data = tableView.getItems();
        data.clear();
        Client client = ClientBuilder.newClient();
        client.register(CustomerMessageBodyReader.class);
        if (textFieldSearch.getText().length() > 0) {
            clientTarget = client.target("http://localhost:8080/ServerSide/webresources/customer/search/{beginBy}");
            clientTarget = clientTarget.resolveTemplate("beginBy", textFieldSearch.getText());
        } else {
            clientTarget = client.target("http://localhost:8080/ServerSide/webresources/customer");
        }   
        GenericType<List<Customer>> listc = new GenericType<List<Customer>>() {
            };
        List<Customer> customers = clientTarget.request("application/json").get(listc);

        for (Customer c : customers) {
            data.add(c);
            System.out.println(c.toString());
        }
    }
    
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        //handleSearchAction();
    }    
    
}
