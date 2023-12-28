import React from 'react'
import { Button, Modal, Form, Input, Message, Icon } from 'semantic-ui-react';
import { apiService } from '../api/apiService';
import { Pharmacy, Response } from '../api/types';
import { useSelector, useDispatch } from 'react-redux'
import { RootState, AppDispatch } from '../store/store'
import { setCurrentPharmacy, updateCurrentPharmacy, setStatus } from '../store/pharmacySlice';

interface PharmacyModalProps {
    closeModal: () => void;
}

const PharmacyModal: React.FC<PharmacyModalProps> = ({ closeModal }) => {

    const dispatch: AppDispatch = useDispatch();
    const updatedPharmacy = useSelector((state: RootState) => state.pharmacies.currentPharmacy);
    const updateError = useSelector((state: RootState) => state.pharmacies.status) == "failed";

    const saveChanges = async () => {
        dispatch(setStatus("loading"))
        try {
            const response = await apiService.updatePharmacy(updatedPharmacy as Pharmacy);
            if ((response as Pharmacy).id !== undefined){
                dispatch(setCurrentPharmacy(response as Pharmacy));
                closeModal();
            }
            else 
            {
                console.log('Error response: ', (response as Response).message);
                dispatch(setStatus("failed"));
            }
        }
        catch(error){
            dispatch(setStatus("idle"))
            console.error('Failed updating: ', error)
        }
        
    }

    const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (updatedPharmacy) {
            dispatch(updateCurrentPharmacy({[event.target.name]: event.target.value}));
        }
    }

    const handleDismiss = () => {
        dispatch(setStatus("idle"));
    }

    return (
        <>
            <Modal
                open={true}
                onClose={closeModal}
                
                >
                <Modal.Header>Pharmacy Data</Modal.Header>
                <Modal.Content>
                    <Message 
                        attached 
                        error 
                        hidden={!updateError}
                        onDismiss={handleDismiss}
                        >
                        <Icon name='exclamation' />
                        There was an error when updating the Pharmacy. Please try again.
                        </Message>
                    <Form>
                        <Form.Field>
                            <label>Name</label>
                            <Input
                                name="name"
                                value={updatedPharmacy?.name}
                                onChange={handleInputChange}
                                />
                        </Form.Field>
                        <Form.Field>
                            <label>Address</label>
                            <Input
                                name="address"
                                value={updatedPharmacy?.address}
                                onChange={handleInputChange}
                                />
                        </Form.Field>
                        <Form.Field>
                            <label>City</label>
                            <Input
                                name="city"
                                value={updatedPharmacy?.city}
                                onChange={handleInputChange}
                                />
                        </Form.Field>
                        <Form.Field>
                            <label>State</label>
                            <Input
                                name="state"
                                value={updatedPharmacy?.state}
                                onChange={handleInputChange}
                                />
                        </Form.Field>
                        <Form.Field>
                            <label>Zip Code</label>
                            <Input
                                name="zip"
                                value={updatedPharmacy?.zip}
                                onChange={handleInputChange}
                                />
                        </Form.Field>
                    </Form>
                </Modal.Content>
                <Modal.Actions>
                    <Button.Group>
                    <Button onClick={closeModal}>Cancel</Button>
                    <Button.Or />
                    <Button positive onClick={saveChanges}>Save</Button>
                    </Button.Group>
                </Modal.Actions>
            </Modal>
        </>
    )

}

export default PharmacyModal