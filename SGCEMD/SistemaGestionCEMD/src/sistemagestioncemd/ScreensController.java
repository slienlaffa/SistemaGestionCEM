/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sistemagestioncemd;

import java.util.HashMap;
import javafx.animation.KeyFrame;
import javafx.animation.KeyValue;
import javafx.animation.Timeline;
import javafx.beans.property.DoubleProperty;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXMLLoader;
import javafx.scene.Node;
import javafx.scene.Parent;
import javafx.scene.layout.StackPane;
import javafx.util.Duration;

/**
 *
 * @author felipe
 */
public class ScreensController extends StackPane{
    
    private HashMap<String, Node> screens = new HashMap<>();
    
    public ScreensController() {
        super();
    }
    
    //añade la entana a la colleccion
    public void addScreen(String name, Node screen){
        screens.put(name, screen);
    }
    
    //Retorna el nodo con su nombre
    public Node getScreen(String name){
        return screens.get(name);
    }
    
    //carga el archivo fxml, agrega la ventana a la coleccion y
    // finalmente inyecta la ventana al controlador
    public boolean loadScreen(String name, String resource){
        try{
            FXMLLoader myLoader = new FXMLLoader(getClass().getResource(resource));
            Parent loadScreen = (Parent) myLoader.load();
            ControlledScreen myScreenControler = ((ControlledScreen) myLoader.getController());
            myScreenControler.setScreenParent(this);
            addScreen(name, loadScreen);
            return true;
        } catch (Exception e){
            System.out.println(e.getMessage());
            return false;
        }
    }
    
    //carga una ventana por su nombre
    //primero revisa si hay una ventana cargada. Luego si hay mas de una
    //la nueva ventana es añadida y la anterior removida
    //si no hay ninguna ventana cargada, la nueva ventana se agrega.
    public boolean setScreen(final String name){
        if(screens.get(name) != null){
            final DoubleProperty opacity = opacityProperty();
            
            if(!getChildren().isEmpty()){
                Timeline fade = new Timeline(
                        new KeyFrame(Duration.ZERO, new KeyValue(opacity, 1.0)),
                        new KeyFrame(new Duration(1000), new EventHandler<ActionEvent>() {
                    @Override
                    public void handle(ActionEvent t){
                        getChildren().remove(0);
                        getChildren().add(0, screens.get(name));
                        Timeline fadeIn = new Timeline(
                            new KeyFrame(Duration.ZERO, new KeyValue(opacity, 0.0)),
                            new KeyFrame(new Duration(800), new KeyValue(opacity, 1.0)));
                        fadeIn.play();
                    }
                }, new KeyValue(opacity, 0.0)));
                fade.play();
            } else {
                setOpacity(0.0);
                //si no hay ventanas solo la mostramos
                getChildren().add(screens.get(name));
                Timeline fadeIn = new Timeline(
                        new KeyFrame(Duration.ZERO, new KeyValue(opacity, 0.0)),
                        new KeyFrame(new Duration(2500), new KeyValue(opacity, 1.0)));
                fadeIn.play();
            }
            return true;
        } else {
            System.out.println("La ventana no ha sido cargada \n");
            return false;
        }
    }
    
    //remueve la ventana de la colleccion por su nombre
    public boolean unloadScreen(String name) {
        if (screens.remove(name) == null){
            System.out.println("La ventana no existe");
            return false;
        } else {
            return true;
        }
    }
}
