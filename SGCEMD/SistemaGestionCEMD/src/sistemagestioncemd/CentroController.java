/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sistemagestioncemd;

import java.net.URL;
import java.util.List;
import java.util.ResourceBundle;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.ComboBox;
import javafx.scene.control.TableView;
import javafx.scene.control.TextArea;
import javafx.scene.control.TextField;
import javax.ws.rs.core.GenericType;

/**
 * FXML Controller class
 *
 * @author felipe
 */
public class CentroController implements Initializable, ControlledScreen {

    ScreensController myController;
    
    @FXML
    private TextField txtNombreInstitucion;
    @FXML
    private TextField txtNombreDirector;
    @FXML
    private TextField txtEspecializacion;
    @FXML
    private TextField txtDireccion;
    @FXML
    private TextField txtNContacto;
    @FXML
    private TextField txtCorreo;
    @FXML
    private ComboBox cbxPais;
    @FXML
    private ComboBox cbxCiudad;
    @FXML
    private TextArea txtDescripcion;
    @FXML
    private TableView<Centro> tCentro;
    
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        // TODO
        //handlerBuscaCentrosAction();
    }    
    
    @Override
    public void setScreenParent(ScreensController screenParent) {
        myController = screenParent;
    }
    
    @FXML
    private void handlerGuardarCentroAction(ActionEvent event) {
        if(true){
            //aqui cosas de guardar
        
            myController.setScreen(SistemaGestionCEMD.screenInicioAdministradorID);
        } else{
            //accion de si no guarda
        }      
    }
    
    @FXML
    private void handlerModificarCentroAction(ActionEvent event) {
        if(true){
            //aqui cosas de modificar
        
            myController.setScreen(SistemaGestionCEMD.screenInicioAdministradorID);
        } else{
            //accion de si no guarda
        }      
    }
    
    @FXML
    private void handlerBuscaCentrosAction() {
        ObservableList<Centro> data = tCentro.getItems();
        data.clear();
        
        GenericType<List<Centro>> listc = new GenericType<List<Centro>>() {
            };
        List<Centro> centros = null;//aqui se carga la lista

        for (Centro c : centros) {
            data.add(c);
            System.out.println(c.toString());
        }   
    }
    
    @FXML
    private void handlerLimpiarAction(ActionEvent event) {
        txtNombreDirector.clear();
        txtEspecializacion.clear();
        txtNombreInstitucion.clear();
        txtCorreo.clear();
        txtDireccion.clear();
        txtNContacto.clear();
        cbxCiudad.getSelectionModel().clearSelection();
        cbxPais.getSelectionModel().clearSelection();
        txtDescripcion.clear();
    }
    
    @FXML
    private void handlerVolverAction(ActionEvent event) {
        myController.setScreen(SistemaGestionCEMD.screenInicioAdministradorID);
    }
    
}
